using System;
using Controllers.Game;
using MyEventBus;
using MyEventBus.Signals.ScoreSignals;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class PipeMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
    
        private bool _isMoving;
        private float _movementDistance;
        private Vector3 _startPosition;
        private PipeSpawner _spawner;
        private EventBus _eventBus;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            if (Camera.main != null) _movementDistance = Camera.main.orthographicSize * 3;
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _isMoving = true;
        }

        private void OnDisable()
        {
            _isMoving = false;
        }

        private void Update()
        {
            if (!_isMoving || _movementDistance == 0f)
                return;
        
            Move();
        }

        private void Move()
        {
            if (transform.position.x < -_movementDistance)
            {
                if (_spawner != null)
                    _spawner.Release(this);
                transform.position = _startPosition;
            }
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }

        public void GetPoolParent(PipeSpawner spawner)
        {
            _spawner = spawner;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _eventBus.Invoke(new ScoreAddedSignal(1));
            }
        }
    }
}
