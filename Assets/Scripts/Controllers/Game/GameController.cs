using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.PlayerSignals;
using UnityEngine;

namespace Controllers.Game
{
    public class GameController : IDisposable
    {
        private EventBus _eventBus;
        
        public GameController(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Init()
        {
            _eventBus.Subscribe<PlayerFallSignal>(OnPlayerFall);
            _eventBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
        }
        
        public void StartGame()
        {
            _eventBus.Invoke(new GameStartedSignal());
            Time.timeScale = 1f;
        }

        public void RestartGame()
        {
            _eventBus.Invoke(new RestartGameSignal());
            Time.timeScale = 0f;
        }

        private void StopGame()
        {
            _eventBus.Invoke(new GameStoppedSignal());
        }

        private void OnPlayerFall(PlayerFallSignal signal)
        {
            StopGame();
        }

        private void OnGameLoaded(GameLoadedSignal signal)
        {
            Time.timeScale = 0f;
        }
        
        public void Dispose()
        {
            _eventBus.Unsubscribe<PlayerFallSignal>(OnPlayerFall);
        }
    }
}
