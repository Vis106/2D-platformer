using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private Movement _movement;

    private Vector3 _currentPosition;
    private bool _movingRight = true;
    private byte _flipCount = 0;

    private void Update()
    {
        _currentPosition = transform.position;
        Patrol(_currentPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EndMapZone _))
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void Patrol(Vector3 currentPosition)
    {
        if (_movingRight)
            _movement.Move(Vector3.right, currentPosition, out Vector3 _);
        else
            _movement.Move(Vector3.left, currentPosition, out Vector3 _);

        if (_groundSensor.IsGrounded())
            _flipCount = 0;

        if (!_groundSensor.IsGrounded() && _flipCount == 0)
        {
            _movement.Flip(ref _movingRight);
            _flipCount = +1;
        }
    }
}