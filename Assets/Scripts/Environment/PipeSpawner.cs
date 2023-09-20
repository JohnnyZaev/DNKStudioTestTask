using Controllers.Game;
using Difficulty;
using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.SettingsSignals;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Environment
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private float timeBetweenSpawns = 2f;
        private ObjectPool<PipeMovement> _pipePool;

        private float _pipeSpeed;
        private float _currentTime;
        private bool _isActive;
        private EventBus _eventBus;
        private DiContainer _diContainer;
        private SettingsController _settingsController;
        private DifficultyBase _currentDifficulty;

        [Inject]
        private void Construct(EventBus eventBus, DiContainer diContainer, SettingsController settingsController)
        {
            _eventBus = eventBus;
            _diContainer = diContainer;
            _settingsController = settingsController;
            
            _eventBus.Subscribe<SettingsChangedSignal>(OnSettingsChanged);
        }

        private void OnSettingsChanged(SettingsChangedSignal obj)
        {
            _currentDifficulty = _settingsController.GetCurrentDifficultyBase();
            _pipeSpeed = _currentDifficulty.pipeSpeed;
            timeBetweenSpawns = _currentDifficulty.timeToSpawn;
        }

        private void Awake()
        {
            _pipePool = new ObjectPool<PipeMovement>((() => _diContainer.InstantiatePrefab(pipePrefab, transform).GetComponent<PipeMovement>()),
                pipe =>
                {
                    var position = pipe.transform.position;
                    var offset = position;
                    offset.y = Random.Range(-_currentDifficulty.middleDistance, _currentDifficulty.middleDistance);
                    position += offset;
                    pipe.transform.position = position;
                    pipe.SetSpeed(_pipeSpeed);
                    pipe.gameObject.SetActive(true);
                    pipe.GetPoolParent(this);
                },
                pipe =>
                {
                    pipe.gameObject.SetActive(false);
                },
                pipe =>
                {
                    Destroy(pipe.gameObject);
                });
        
            //temp
            _isActive = true;
            _currentTime = timeBetweenSpawns;
        }

        public void Release(PipeMovement pipe)
        {
            _pipePool.Release(pipe);
        }

        private void Update()
        {
            if (!_isActive)
                return;

            _currentTime += Time.deltaTime;
            if (_currentTime > timeBetweenSpawns)
            {
                _pipePool.Get();
                _currentTime = 0;
            }
        }
    }
}
