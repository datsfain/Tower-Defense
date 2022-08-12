using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnValueChanged;
    private Enemy m_Enemy;

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
                GameEvents.OnEnemyKilled(m_Enemy);
                Destroy(gameObject);
            }
        }
    }
    public int MaxHealth { get; set; }

    private void Awake()
    {
        m_Enemy = GetComponent<Enemy>();
        CurrentHealth = MaxHealth = m_Enemy.Stats.MaxHealth;
    }
}
