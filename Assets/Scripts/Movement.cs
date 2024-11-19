using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    public void Move(Vector3 direction, Vector3 currentPosition, out Vector3 between)
    {
        Vector3 move = new Vector3(direction.x, direction.y);

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 targetPosition = currentPosition + move;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, scaledMoveSpeed);

        transform.position = newPosition;

        between = newPosition - currentPosition;
    }

    public void Jump(Rigidbody2D rigidbody, bool isGrounded)
    {
        if (isGrounded)
        {
            rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        }
    }

    public void Flip(ref bool movingRight)
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        movingRight = !movingRight;
    }
}