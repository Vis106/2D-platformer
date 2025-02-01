using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Player _target;
    [SerializeField] private float _chaseCheckRadius = 3f;
    [SerializeField] private float _attackCheckRadius = 3f;
    [SerializeField] private float _maxEnemyHealth = 100;
    [SerializeField] private Health _enemyHealth;
    [SerializeField] private EnemyCombat _enemyCombat;
    [SerializeField] private float _attackRecharge = 1f;

    private Vector3 _currentPosition;
    private Vector3 targetPosition;
    private float _startRecharge;

    private void Awake()
    {
        _startRecharge = _attackRecharge;
    }

    private void Update()
    {
        _currentPosition = transform.position;
        targetPosition = _target.transform.position;

        if (Vector2.Distance(_target.transform.position, transform.position) < _chaseCheckRadius)
        {
            targetPosition = _target.transform.position;
            _movement.Chase(_currentPosition, targetPosition);
            Debug.Log("Chase");

            if (Vector2.Distance(_target.transform.position, transform.position) < _attackCheckRadius)
            {
                if (IsReady())
                {
                    _enemyCombat.Attack();
                    _attackRecharge = 0;
                }
            }
        }
        else
        {
            _movement.Patrol(_currentPosition);
            Debug.Log("Patrol");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EndMapZone _))
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
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

    public void TakeDamage(float damage)
    {
        _enemyHealth.AplayDamage(damage);
    }
}