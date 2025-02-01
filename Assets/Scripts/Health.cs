using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    public event Action<float> ValueChanged;        

    public void AplayDamage(float damage)
    {
        _value -= damage;
        ValueChanged?.Invoke(_value);
        Debug.Log(_value);       

        if (_value <= 0)
        {
            Die();
        }
    }

    public void AplayHeal(float heal)
    {
        _value += heal;
        Debug.Log(_value);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
