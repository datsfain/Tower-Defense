using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Scriptable Objects/EnemyType")]
public class EnemyTypeSO : ScriptableObject
{
    [field: SerializeField]
    public Enemy EnemyPrefab;

    [field: SerializeField]
    public float AttackInterval;

    [field: SerializeField]
    public float AttackRange;

    [field: SerializeField] 
    public int MaxHealth;

    [field: SerializeField, Range(1f, 20f)] 
    public int AttackDamage;

    [field: SerializeField, Range(1f, 4f)] 
    public float MoveSpeed;

    [field: SerializeField] 
    public int MinRewardCoins;
    [field: SerializeField] 
    public int MaxRewardCoins;
}
