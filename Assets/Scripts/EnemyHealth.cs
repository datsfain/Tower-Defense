using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnValueChanged;
    private EnemyTypeSO m_Stats;

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
            if(_currentHealth <= 0)
            {
                GameEvents.OnEnemyKilled(m_Stats);
                Destroy(gameObject);
            }
        }
    }
    public int MaxHealth { get; set; }

    private void Awake()
    {
        m_Stats = GetComponent<Enemy>().Stats;
        CurrentHealth = MaxHealth = m_Stats.MaxHealth;
    }
}
