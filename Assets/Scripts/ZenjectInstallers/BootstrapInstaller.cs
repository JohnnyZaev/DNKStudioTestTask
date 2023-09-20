using Controllers.Game;
using Difficulty;
using MyEventBus;
using UnityEngine;
using Utils;
using Zenject;

namespace ZenjectInstallers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private DifficultyBase[] difficultyArray;
        private EventBus _eventBus;
        [SerializeField] private AudioSource audioSource;
        

        private SettingsController _settingsController;
        public override void InstallBindings()
        {
            InitServices();
            Container.Bind<EventBus>().FromInstance(_eventBus).AsSingle();
            Container.Bind<SettingsController>().FromInstance(_settingsController).AsSingle();
            Container.Bind<AudioSource>().FromInstance(audioSource).AsSingle().NonLazy();
        }
        
        private void InitServices()
        {
            _eventBus = new EventBus();
            _settingsController = new SettingsController(_eventBus, audioSource, PlayerPrefs.GetInt(StringConstants.Difficulty, 0));
            _settingsController.Init(difficultyArray);
        }
    }
}