using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public readonly List<Enemy> Enemies = new List<Enemy>();

    private void OnEnable()
    {
        GameEvents.OnEnemySpawned += AddEnemy;
        GameEvents.OnEnemyKilled += RemoveEnemy;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemySpawned -= AddEnemy;
        GameEvents.OnEnemyKilled -= RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
    private void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }
}
