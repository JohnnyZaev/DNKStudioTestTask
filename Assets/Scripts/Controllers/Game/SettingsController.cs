using Difficulty;
using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.SettingsSignals;
using UnityEngine;
using Utils;
using Zenject;

namespace Controllers.Game
{
    public class SettingsController : IDisposable
    {
        public int CurrentDifficulty { get; private set; }
        public float CurrentVolume { get; private set; }
    
        private EventBus _eventBus;
        private DifficultyBase[] _difficultyArray;
        [Inject] private AudioSource _audioSource;
        public SettingsController(EventBus eventBus, AudioSource audioSource, int currentDifficulty = 0)
        {
            _eventBus = eventBus;
            CurrentDifficulty = currentDifficulty;
            _audioSource = audioSource;
            _eventBus.Subscribe<SettingsChangedSignal>(OnSettingsChanged);
            _eventBus.Subscribe<VolumeChangedSignal>(OnVolumeChanged);
        }

        private void OnVolumeChanged(VolumeChangedSignal obj)
        {
            CurrentVolume = obj.Volume;
            _audioSource.volume = CurrentVolume;
            PlayerPrefs.SetFloat(StringConstants.Volume, CurrentVolume);
        }

        private void OnSettingsChanged(SettingsChangedSignal obj)
        {
            CurrentDifficulty = obj.Difficulty;
            PlayerPrefs.SetInt(StringConstants.Difficulty, CurrentDifficulty);
        }

        public void Init(DifficultyBase[] difficultyBases)
        {
            _difficultyArray = difficultyBases;
            _eventBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
        }

        public DifficultyBase GetCurrentDifficultyBase()
        {
            return _difficultyArray[CurrentDifficulty];
        }

        public void ChangeVolume(float volume)
        {
            CurrentVolume = volume;
            _audioSource.volume = CurrentVolume;
        }

        private void OnGameLoaded(GameLoadedSignal obj)
        {
            _eventBus.Invoke(new SettingsChangedSignal(CurrentDifficulty));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);
        }
    }
}
