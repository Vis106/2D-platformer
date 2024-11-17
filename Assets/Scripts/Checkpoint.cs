using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    public void ReturnToStart(Player player)
    {
        player.transform.position = _startPoint.position;
    }
}
