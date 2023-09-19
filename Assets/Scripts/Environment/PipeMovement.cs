using System;
using Unity.VisualScripting;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    

    private bool _isMoving;
    private float _movementDistance;

    private void Awake()
    {
        if (Camera.main != null) _movementDistance = Camera.main.orthographicSize * 3;
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
            gameObject.SetActive(false);
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}
