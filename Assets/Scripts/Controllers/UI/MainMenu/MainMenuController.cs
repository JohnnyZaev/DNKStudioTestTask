using MyEventBus;
using MyEventBus.Signals.SettingsSignals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Controllers.UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        
        [SerializeField] private Button startButton;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Dropdown settingsDropdown;

        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void Awake()
        {
            startButton.onClick.AddListener(LoadGame);
            settingsDropdown.onValueChanged.AddListener(ChangeSettings);
            settingsDropdown.value = PlayerPrefs.GetInt(StringConstants.Difficulty, 0);
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
            volumeSlider.value = PlayerPrefs.GetFloat(StringConstants.Volume, 0.5f);
        }

        private void ChangeVolume(float volume)
        {
            _eventBus.Invoke(new VolumeChangedSignal(volume));
        }

        private void ChangeSettings(int difficulty)
        {
            _eventBus.Invoke(new SettingsChangedSignal(difficulty));
        }


        private void OnDisable()
        {
            startButton.onClick.RemoveListener(LoadGame);
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(StringConstants.GameSceneName);
        }
    }
}
