using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundDetection;
    [SerializeField] private LayerMask _groundCheckMask;

    private Vector3 _currentPosition;
    private bool _movingRight = true;

    private const float GroundCheckRadius = 0.2F;

    private void Update()
    {
        _currentPosition = transform.position;

        if (_movingRight)
            Movement.Move(_speed, Vector3.right, _currentPosition, gameObject, out Vector3 between);
        else
            Movement.Move(_speed, Vector3.left, _currentPosition, gameObject, out Vector3 between);

        if (!ControlGround.CheckGround(_groundDetection, _groundCheckMask, GroundCheckRadius))
            Movement.Flip(gameObject, ref _movingRight);
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
}