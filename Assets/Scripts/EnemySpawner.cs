using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyTypeSO[] enemyTypes;
    public Transform castle;

    public float spawnInterval;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
            var enemy = Instantiate(enemyType.EnemyPrefab, transform.position, Quaternion.identity);
            enemy.SetTarget(castle.position);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
