using UnityEngine;

public class EndMapZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ReturnToStart();
        }
    }
}