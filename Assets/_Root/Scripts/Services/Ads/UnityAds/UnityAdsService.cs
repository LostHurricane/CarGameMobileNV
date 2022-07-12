using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using System;
using Tool;

namespace Services.Ads.UnityAds
{
    internal class UnityAdsService : Service<UnityAdsService> , IUnityAdsInitializationListener, IAdsService, IDisposable
    {
        private readonly ResourcePath _settingsPath = new ResourcePath("Services/UnityAdsSettings");

        private UnityAdsSettings _settings;
        
        public UnityEvent Initialized { get; private set; }

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;

        public UnityAdsService () : base ()
        {
            Initialized = new UnityEvent();

            _settings = ResourcesLoader.LoadScriptableObject<UnityAdsSettings>(_settingsPath);
            Initialized.AddListener(InitializePlayers);
            InitializeAds();
        }



        private void InitializeAds() =>
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                _settings.EnablePerPlacementMode,
                this);

        private void InitializePlayers()
        {
            Log($"Init players");
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Interstitial.Enabled
                ? new InterstitialPlayer(_settings.Interstitial.Id)
                : new StubPlayer("");

        private IAdsPlayer CreateRewarded() =>
            _settings.Rewarded.Enabled
                ? new RewardedPlayer (_settings.Interstitial.Id)
                :new StubPlayer("");

        private IAdsPlayer CreateBanner() =>
            new StubPlayer("");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            Log("Initialization complete.");
            Initialized?.Invoke();
            
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Error($"Initialization Failed: {error.ToString()} - {message}");

        public void Dispose()
        {
            Initialized.RemoveListener(InitializePlayers);

            if (InterstitialPlayer is IDisposable interstitialPlayer)
            {
                interstitialPlayer.Dispose();
            }

            if (RewardedPlayer is IDisposable rewardedPlayer)
            {
                rewardedPlayer.Dispose();
            }

            if (BannerPlayer is IDisposable bannerPlayer)
            {
                bannerPlayer.Dispose();
            }

        }

        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
