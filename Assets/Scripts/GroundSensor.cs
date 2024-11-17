using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    private const float GroundCheckRadius = 0.2F;

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundCheckMask;

    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckPoint.position, GroundCheckRadius, _groundCheckMask);

        return colliders.Length > 0;
    }
}