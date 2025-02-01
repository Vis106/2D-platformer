using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Movement _movement;
    [SerializeField] private Health _playerHealth;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _movingRight = true;
    private Vector3 _direction;
    private Vector3 _currentPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _currentPosition = transform.position;
        _direction = _playerInput.InputDirection();

        _movement.Move(_direction, _currentPosition, out Vector3 between);
        Animate(between.magnitude);
        Animate(_groundSensor.IsGrounded());

        _movement.ChangeDirection(_direction);
        
        if (_direction.y > 0)
            _movement.Jump(_rigidBody, _groundSensor.IsGrounded());
    }

    private void Animate(float speed)
    {
        _animator.SetFloat(HashAnimationNames.PlayerAnimation.SpeedHash, speed);
    }

    private void Animate(bool isGrounded)
    {
        _animator.SetBool(HashAnimationNames.PlayerAnimation.IsGroundedHash, isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            coin.Collect();
        }       
    }

    public void TakeHeal(float heal)
    {
        _playerHealth.AplayHeal(heal);        
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.AplayDamage(damage);
    }
}