using System;
using System.Collections.Generic;
using Controllers.Game;
using MyEventBus.Signals.GameSignals;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using EventBus = MyEventBus.EventBus;
using IDisposable = MyEventBus.IDisposable;

namespace Controllers
{
    public class EntryPoint : MonoBehaviour
    {
        private EventBus _eventBus;
        private GameController _gameController;
        private ScoreController _scoreController;
        private SettingsController _settingsController;
        
        private List<IDisposable> _disposables = new List<IDisposable>();

        [Inject]
        private void Construct(GameController gameController, ScoreController scoreController, SettingsController settingsController, EventBus eventBus)
        {
            _gameController = gameController;
            _scoreController = scoreController;
            _settingsController = settingsController;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            Init();
            AddDisposables();
            
            _eventBus.Invoke(new GameLoadedSignal());
        }

        private void Init()
        {
            _gameController.Init();
            _scoreController.Init();
        }
        
        private void AddDisposables()
        {
            _disposables.Add(_gameController);
            _disposables.Add(_settingsController);
            _disposables.Add(_scoreController);
        }

        private void OnDestroy()
        {
            foreach (var obj in _disposables)
            {
                obj.Dispose();
            }
        }
    }
}
