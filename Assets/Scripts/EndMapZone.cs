using UnityEngine;

public class EndMapZone : MonoBehaviour
{
    [SerializeField] private Checkpoint _checkpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _checkpoint.ReturnToStart(player);
        }
    }
}