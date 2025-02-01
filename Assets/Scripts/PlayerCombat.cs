using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackDamage = 40f;
    [SerializeField] private float _attackRecharge = 1f;

    private Animator _animator;
    private float _startRecharge;

    private void Awake()
    {
        _startRecharge = _attackRecharge;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsReady())
            if (Input.GetKeyDown(KeyCode.Space))           
                Attack(); 
    }

    private void Attack()
    {
        _animator.SetTrigger(HashAnimationNames.PlayerAnimation.AttackHash);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.GetComponent<Health>().AplayDamage(_attackDamage);
            }
        }

        _attackRecharge = 0;
    }

    private bool IsReady()
    {
        if (_attackRecharge >= _startRecharge)
        {
            return true;
        }
        else
        {
            _attackRecharge += Time.deltaTime;
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
