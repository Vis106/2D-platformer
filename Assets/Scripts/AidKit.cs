using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float _healValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeHeal(_healValue);
            gameObject.SetActive(false);
        }
    }
}
