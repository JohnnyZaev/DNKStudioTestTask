using UnityEngine;

namespace Environment
{
    public class PipeMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
    
        private bool _isMoving;
        private float _movementDistance;
        private Vector3 _startPosition;
        private PipeSpawner _spawner;

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
    }
}
