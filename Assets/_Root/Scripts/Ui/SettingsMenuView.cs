using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction ToMainMenu) =>
            _buttonBack.onClick.AddListener(ToMainMenu);

        public void OnDestroy() =>
            _buttonBack.onClick.RemoveAllListeners();
    }
}
