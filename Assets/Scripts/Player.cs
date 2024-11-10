using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundCheckMask;

    private const float GroundCheckRadius = 0.2F;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _movingRight = false;
    private Vector3 _startPosition;

    private Vector2 _direction;
    private Vector3 _currentPosition;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        _currentPosition = transform.position;

        Movement.Move(_moveSpeed, SetDirection(), _currentPosition, gameObject, out Vector3 between);
        Animate(between.magnitude);
        Animate(ControlGround.CheckGround(_groundCheckPoint, _groundCheckMask, GroundCheckRadius));

        if (SetDirection().x < 0 && !_movingRight)
        {
            Movement.Flip(gameObject, ref _movingRight);
            Debug.Log(SetDirection().x);
            Debug.Log(_movingRight);
        }
        else if (SetDirection().x > 0 && _movingRight)
        {
            Movement.Flip(gameObject, ref _movingRight);
        }

        if (SetDirection().y > 0)
            Movement.Jump(_rigidBody, _jumpForce, ControlGround.CheckGround(_groundCheckPoint, _groundCheckMask, GroundCheckRadius));
    }

    public void ReturnToStart()
    {
        transform.position = _startPosition;
    }

    private void Animate(float speed)
    {
        _animator.SetFloat(HashAnimationNames.PlayerAnimation.SpeedHash, speed);
    }

    private void Animate(bool isGrounded)
    {
        _animator.SetBool(HashAnimationNames.PlayerAnimation.IsGroundedHash, isGrounded);
    }

    private Vector3 SetDirection()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f);
        inputDirection = transform.TransformDirection(inputDirection);

        return inputDirection;
    }
}