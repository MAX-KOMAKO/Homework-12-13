using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _height = 3f;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _smoothTime = 0.15f;
    [SerializeField] private float _directionSmoothTime = 0.1f;

    private Rigidbody _targetRigidbody;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _currentDirection = Vector3.forward;

    private void Awake()
    {
        if (_target != null)
        {
            _targetRigidbody = _target.GetComponent<Rigidbody>();
        }
    }

    private void LateUpdate()
    {
        if (_target == null || _targetRigidbody == null) return;

        Vector3 targetDirection = _targetRigidbody.velocity.normalized;
        if (targetDirection.sqrMagnitude < 0.01f)
        {
            targetDirection = _currentDirection;
        }

        _currentDirection = Vector3.SmoothDamp(_currentDirection, targetDirection, ref _velocity, _directionSmoothTime);
        _currentDirection.Normalize();

        Vector3 targetPosition = _target.position - _currentDirection * _distance + Vector3.up * _height;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        transform.LookAt(_target);
    }
}