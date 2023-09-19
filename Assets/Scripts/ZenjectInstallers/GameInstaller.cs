using Controllers.Game;
using MyEventBus;
using Zenject;

namespace ZenjectInstallers
{
    public class GameInstaller : MonoInstaller
    {
        private EventBus _eventBus;
        private GameController _gameController;
        private ScoreController _scoreController;
        
        public override void InstallBindings()
        {
            InitServices();
            Container.Bind<EventBus>().FromInstance(_eventBus).AsSingle();
            Container.Bind<GameController>().FromInstance(_gameController).AsSingle();
            Container.Bind<ScoreController>().FromInstance(_scoreController).AsSingle();
        }

        private void InitServices()
        {
            _eventBus = new EventBus();
            _gameController = new GameController(_eventBus);
            _scoreController = new ScoreController(_eventBus);
        }
    }
}