using UnityEngine;

public static class Movement
{
    public static void Move(float _moveSpeed, Vector3 direction, Vector3 _currentPosition, GameObject gameObject, out Vector3 between)
    {
        Vector3 move = new Vector3(direction.x, direction.y);

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 targetPosition = _currentPosition + move;
        Vector3 newPosition = Vector3.MoveTowards(_currentPosition, targetPosition, scaledMoveSpeed);

        gameObject.transform.position = newPosition;

        between = newPosition - _currentPosition;
    }

    public static void Jump(Rigidbody2D rigidBody, float jumpForce, bool isGrounded)
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }

    public static void Flip(GameObject gameObject, ref bool movingRight)
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        movingRight = !movingRight;
    }
}