using Profile;
using Services.Ads.UnityAds;
using System;
using Tool;
using UnityEngine;

using Services.IAP;
using Object = UnityEngine.Object;


namespace Ui
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/SettingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;
        private IAPService iAPService;


        public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, BuyStuff, ShowAds);
            Debug.Log($"iAPService {IAPService.Instance.IsInitialized}");
        }

        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Start;

        private void BuyStuff() =>
            IAPService.Instance.Buy("1123");
            

        private void ShowAds()
        {
            if (UnityAdsService.Instance.IsInitialized) ShowRewardAds();
            else UnityAdsService.Instance.Initialized.AddListener(ShowRewardAds);
        }

        protected override void OnDispose()
        {
            UnityAdsService.Instance.Initialized.RemoveListener(ShowRewardAds);
        }

        private void ShowRewardAds() => UnityAdsService.Instance.InterstitialPlayer.Play();

    }
}
