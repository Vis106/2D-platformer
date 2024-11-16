using UnityEngine;

public static class GroundSensor
{
    public static bool CheckGround(Transform _groundCheckPoint, LayerMask _groundCheckMask, float GroundCheckRadius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckPoint.position, GroundCheckRadius, _groundCheckMask);

        return colliders.Length > 0;
    }
}