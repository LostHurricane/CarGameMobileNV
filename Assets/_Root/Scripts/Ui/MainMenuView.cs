using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Button _buttonAds;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction BuyStuff, UnityAction WatchAds)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonBuy.onClick.AddListener(BuyStuff);
            _buttonAds.onClick.AddListener(WatchAds);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonAds.onClick.RemoveAllListeners();
        }
    }
}
