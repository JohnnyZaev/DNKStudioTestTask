using Controllers.Game;
using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.ScoreSignals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Controllers.UI.Game
{
    public class GameUIHandler : MonoBehaviour
    {
        [SerializeField] private Button gameStartButton;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private TMP_Text currentScoreText;
        [SerializeField] private TMP_Text bestScoreText;
        
        private EventBus _eventBus;
        private GameController _gameController;
        private ScoreController _scoreController;

        [Inject]
        private void Construct(EventBus eventBus, GameController gameController, ScoreController scoreController)
        {
            _eventBus = eventBus;
            _gameController = gameController;
            _scoreController = scoreController;
            
            _eventBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
            _eventBus.Subscribe<GameStoppedSignal>(OnGameStopped);
            _eventBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
            
            currentScoreText.text = $"Score : 0";
            bestScoreText.text = $"Score : {PlayerPrefs.GetInt(StringConstants.MaxScore, 0)}";
        }

        private void OnScoreChanged(ScoreChangedSignal obj)
        {
            currentScoreText.text = $"Score : {obj.Score}";
            bestScoreText.text = $"Score : {PlayerPrefs.GetInt(StringConstants.MaxScore, 0)}";
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
            _eventBus.Invoke(new RestartGameSignal());
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
