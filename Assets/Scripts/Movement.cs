using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    public void Move(Vector3 direction, Vector3 _currentPosition, GameObject gameObject, out Vector3 between)
    {
        Vector3 move = new Vector3(direction.x, direction.y);

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 targetPosition = _currentPosition + move;
        Vector3 newPosition = Vector3.MoveTowards(_currentPosition, targetPosition, scaledMoveSpeed);

        gameObject.transform.position = newPosition;

        between = newPosition - _currentPosition;
    }

    public void Jump(Rigidbody2D rigidBody, bool isGrounded)
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        }
    }

    public void Flip(GameObject gameObject, ref bool movingRight)
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        movingRight = !movingRight;
    }
}