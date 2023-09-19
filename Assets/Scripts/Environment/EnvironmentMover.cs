using MyEventBus;
using MyEventBus.Signals.GameSignals;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class EnvironmentMover : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float objectWidth;

        private bool _isMoving;
        private Vector3 _startingPosition;
        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
            _startingPosition = transform.position;
            
            _eventBus.Subscribe<GameStartedSignal>(OnGameStarted);
            _eventBus.Subscribe<GameStoppedSignal>(OnGameStopped);
        }

        private void OnGameStarted(GameStartedSignal signal)
        {
            _isMoving = true;
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            Move();
        }

        private void Move()
        {
            if (transform.position.x > -objectWidth)
            {
                transform.Translate(Vector3.left * (speed * Time.deltaTime));
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
        
        private void OnGameStopped(GameStoppedSignal signal)
        {
            _isMoving = false;
        }
    }
}
