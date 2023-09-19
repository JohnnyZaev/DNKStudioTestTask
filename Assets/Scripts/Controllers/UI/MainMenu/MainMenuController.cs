using UnityEngine;
using UnityEngine.UI;
using Utils;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;

        private void Awake()
        {
            startButton.onClick.AddListener(LoadGame);
            settingsButton.onClick.AddListener(OpenSettings);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(LoadGame);
            settingsButton.onClick.RemoveListener(OpenSettings);
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(StringConstants.GameSceneName);
        }


        private void OpenSettings()
        {
            
        }
    }
}
