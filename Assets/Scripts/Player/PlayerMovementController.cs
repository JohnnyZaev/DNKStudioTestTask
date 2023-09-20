using MyEventBus;
using MyEventBus.Signals.GameSignals;
using MyEventBus.Signals.PlayerSignals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float velocity = 1.5f;
        [SerializeField] private float rotationSpeed = 10f;

        private Rigidbody2D _playerRigidbody2D;
        private PlayerInput _playerInput;
        private InputAction _flyAction;
        private EventBus _eventBus;
        private Vector3 _startPosition;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
            
            _eventBus.Subscribe<GameStartedSignal>(OnGameStarted);
            _eventBus.Subscribe<GameStoppedSignal>(OnGameStopped);
            _eventBus.Subscribe<RestartGameSignal>(OnGameRestart);
        }
        
        private void Awake()
        {
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _flyAction = _playerInput.actions.FindAction("Flap");
            _startPosition = transform.position;
        }

        private void OnGameStarted(GameStartedSignal signal)
        {
            _flyAction.Enable();
            _flyAction.performed += MoveUp;
            MoveUp(new InputAction.CallbackContext());
        }
        
        private void OnGameRestart(RestartGameSignal signal)
        {
            var transform1 = transform;
            transform1.position = _startPosition;
            transform1.rotation = Quaternion.identity;
        }

        private void OnGameStopped(GameStoppedSignal signal)
        {
            _flyAction.performed -= MoveUp;
            _flyAction.Disable();
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, _playerRigidbody2D.velocity.y * rotationSpeed);
        }
        
        private void MoveUp(InputAction.CallbackContext obj)
        {
            _playerRigidbody2D.velocity = Vector2.up * velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _eventBus.Invoke(new PlayerFallSignal());
        }
    }
}
