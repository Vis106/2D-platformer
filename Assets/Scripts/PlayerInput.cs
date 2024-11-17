using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public Vector3 InputDirection()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f);
        inputDirection = transform.TransformDirection(inputDirection);

        return inputDirection;
    }
}
