using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamageApplier : MonoBehaviour
{
    public IntVariable CurrentHealth;
    public IntVariable MaxHealth;

    private void Awake()
    {
        CurrentHealth.Value = MaxHealth.Value;
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDamageCastle += DamageCastle;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDamageCastle -= DamageCastle;
    }

    private void DamageCastle(Enemy enemy)
    {
        CurrentHealth.Value -= enemy.Stats.AttackDamage;
    }
}
