using MyEventBus;
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

        private float _currentTime;
        private bool _isActive;
        private EventBus _eventBus;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(EventBus eventBus, DiContainer diContainer)
        {
            _eventBus = eventBus;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _pipePool = new ObjectPool<PipeMovement>((() => _diContainer.InstantiatePrefab(pipePrefab, transform).GetComponent<PipeMovement>()),
                pipe =>
                {
                    pipe.gameObject.SetActive(true);
                    pipe.GetPoolParent(this);
                },
                pipe =>
                {
                    pipe.gameObject.SetActive(false);
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
