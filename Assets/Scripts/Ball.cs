using UnityEngine;

public class Ball : MonoBehaviour
{
    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    [SerializeField] private float _moveForce = 5f;
    [SerializeField] private float _jumpForce = 3f;
    
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rigidbody;
    private bool _jumpRequested;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (_jumpRequested)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpRequested = false;
        }

        float horizontal = Input.GetAxisRaw(_horizontalAxis);
        float vertical = Input.GetAxisRaw(_verticalAxis);

        Vector3 forward = _cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = _cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 movement = (forward * vertical + right * horizontal) * _moveForce;
        _rigidbody.AddForce(movement, ForceMode.Force);
    }

    public void ResetBall(Vector3 startPosition)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = startPosition;

        _jumpRequested = false;
        _rigidbody.WakeUp();
    }
}