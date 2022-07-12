using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        [SerializeField] private Button _buttonBuy;

        [SerializeField] private Button _buttonAds;


        public void Init(UnityAction ToMainMenu, UnityAction BuyStuff, UnityAction ToWatchAds)
        {
            _buttonBack.onClick.AddListener(ToMainMenu);
            _buttonBuy.onClick.AddListener(BuyStuff);
            _buttonAds.onClick.AddListener(ToWatchAds);
            

        }

        private void BuyStuff() =>
    Debug.Log("You just bougt stuff");

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonAds.onClick.RemoveAllListeners();
        }
    }
}
