using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnValueChanged;

    private int _currentHealth;

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            OnValueChanged?.Invoke();
            if(_currentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public int MaxHealth { get; set; }

    private void Awake()
    {
        var stats = GetComponent<Enemy>().Stats;
        CurrentHealth = MaxHealth = stats.MaxHealth;
    }
}
