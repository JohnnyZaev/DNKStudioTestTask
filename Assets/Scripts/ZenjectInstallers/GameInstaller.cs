using Controllers.Game;
using MyEventBus;
using Zenject;

namespace ZenjectInstallers
{
    public class GameInstaller : MonoInstaller
    {
        
        private GameController _gameController;
        private ScoreController _scoreController;
        [Inject] private EventBus _eventBus;
        
        public override void InstallBindings()
        {
            InitServices();
            Container.Bind<GameController>().FromInstance(_gameController).AsSingle();
            Container.Bind<ScoreController>().FromInstance(_scoreController).AsSingle();
        }

        private void InitServices()
        {
            _gameController = new GameController(_eventBus);
            _scoreController = new ScoreController(_eventBus);
        }
    }
}