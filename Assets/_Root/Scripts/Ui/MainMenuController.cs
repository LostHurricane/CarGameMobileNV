using Profile;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;
using Services.Ads.UnityAds;


namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        private IAPService iAPService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, BuyStuff, ShowAds);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void OpenSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void BuyStuff() =>
            IAPService.Instance.Buy("1123");


        private void ShowAds()
        {
            if (UnityAdsService.Instance.IsInitialized) ShowRewardAds();
            else UnityAdsService.Instance.Initialized.AddListener(ShowRewardAds);
        }

        private void ShowRewardAds() => UnityAdsService.Instance.RewardedPlayer.Play();

        protected override void OnDispose()
        {
            UnityAdsService.Instance.Initialized.RemoveListener(ShowRewardAds);
        }
    }
}
