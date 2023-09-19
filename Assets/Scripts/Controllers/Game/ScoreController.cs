using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.PlayerSignals;
using MyEventBus.Signals.ScoreSignals;
using UnityEngine;
using Utils;
using Zenject;

namespace Controllers.Game
{
    public class ScoreController : IDisposable
    {
        private EventBus _eventBus;

        public int Score { get; private set; }

        public ScoreController(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Init()
        {       
            _eventBus.Subscribe<GameStartedSignal>(OnGameStarted);
            _eventBus.Subscribe<ScoreAddedSignal>(OnScoreAdded);
            _eventBus.Subscribe<GameStoppedSignal>(OnGameStopped);
        }

        private void OnGameStarted(GameStartedSignal signal)
        {
            Score = 0;
            _eventBus.Invoke(new ScoreChangedSignal(Score));
        }

        private void OnScoreAdded(ScoreAddedSignal signal)
        {
            Score += signal.Value;
            _eventBus.Invoke(new ScoreChangedSignal(Score));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
            _eventBus.Unsubscribe<ScoreAddedSignal>(OnScoreAdded);
        }
        
        private void OnGameStopped(GameStoppedSignal signal)
        {
            var maxScore = GetMaxScore();
            if (Score > maxScore)
            {
                PlayerPrefs.SetInt(StringConstants.MaxScore, Score);
            }
        }
        
        public int GetMaxScore()
        {
            return PlayerPrefs.GetInt(StringConstants.MaxScore, 0);
        }
    }
}
