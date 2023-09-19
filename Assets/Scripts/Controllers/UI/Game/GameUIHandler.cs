using Controllers.Game;
using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.PlayerSignals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.Game
{
    public class GameUIHandler : MonoBehaviour
    {
        [SerializeField] private Button gameStartButton;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private Button playAgainButton;
        
        private EventBus _eventBus;
        private GameController _gameController;

        [Inject]
        private void Construct(EventBus eventBus, GameController gameController)
        {
            _eventBus = eventBus;
            _gameController = gameController;
            
            _eventBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
            _eventBus.Subscribe<GameStoppedSignal>(OnGameStopped);
        }

        private void OnGameLoaded(GameLoadedSignal signal)
        {
            gameStartButton.onClick.AddListener(StartGame);
            playAgainButton.onClick.AddListener(RestartGame);
            gameStartButton.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            gameStartButton.gameObject.SetActive(false);
            _gameController.StartGame();
        }

        private void RestartGame()
        {
            CloseAllWindows();
            _gameController.RestartGame();
            gameStartButton.gameObject.SetActive(true);
        }

        private void OnGameStopped(GameStoppedSignal signal)
        {
            // Open results UI
            gameOverUI.SetActive(true);
        }

        private void CloseAllWindows()
        {
            // CloseAllWindows
            gameOverUI.SetActive(false);
        }
    }
}
