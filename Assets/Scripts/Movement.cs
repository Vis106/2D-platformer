using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundSensor _groundSensor;

    private bool _movingRight = true;
    private byte _flipCount = 0;

    public void Move(Vector3 direction, Vector3 currentPosition, out Vector3 between)
    {
        Vector3 move = new Vector3(direction.x, direction.y);

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 targetPosition = currentPosition + move;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, scaledMoveSpeed);

        transform.position = newPosition;

        between = newPosition - currentPosition;
    }

    public void ChangeDirection(Vector3 direction)
    {
        if (direction.x < 0 && !_movingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && _movingRight)
        {
            Flip();
        }       
    }

    public void Jump(Rigidbody2D rigidbody, bool isGrounded)
    {
        if (isGrounded)
        {
            rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        }
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        _movingRight = !_movingRight;
    }

    public void Patrol(Vector3 currentPosition)
    {
        if (_movingRight)
            Move(Vector3.right, currentPosition, out Vector3 _);
        else
            Move(Vector3.left, currentPosition, out Vector3 _);

        if (_groundSensor.IsGrounded())
            _flipCount = 0;

        if (!_groundSensor.IsGrounded() && _flipCount == 0)
        {
            Flip();
            _flipCount = +1;
        }
    }

    public void Chase(Vector3 currentPosition, Vector3 target)
    {
        if (target.x < currentPosition.x)
        {
            if (transform.localScale.x > 0)
                Flip();
        }
        else
        {
            if (transform.localScale.x < 0)
                Flip();
        }

        transform.position = Vector3.MoveTowards(currentPosition, target, _moveSpeed * Time.deltaTime);
    }
}