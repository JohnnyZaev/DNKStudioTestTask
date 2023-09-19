using Controllers.Game;
using MyEventBus;
using MyEventBus.Signals.GameSignals;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class EntryPoint : MonoBehaviour
    {
        private EventBus _eventBus;
        private GameController _gameController;
        private ScoreController _scoreController;

        [Inject]
        private void Construct(GameController gameController, ScoreController scoreController, EventBus eventBus)
        {
            _gameController = gameController;
            _scoreController = scoreController;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            Init();
            
            _eventBus.Invoke(new GameLoadedSignal());
        }

        private void Init()
        {
            _gameController.Init();
            _scoreController.Init();
        }
    }
}
