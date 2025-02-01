using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackDamage = 40f;

    private Animator _animator;    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger(HashAnimationNames.PlayerAnimation.SnakeAttackHash);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D collider in hitPlayer)
        {
            if (collider.TryGetComponent(out Player player))
            {
                player.GetComponent<Health>().AplayDamage(_attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
