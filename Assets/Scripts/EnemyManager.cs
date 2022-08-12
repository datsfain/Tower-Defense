using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyWaveSO[] m_Waves;
    [SerializeField] private Transform m_Castle;
    [SerializeField] private float TimeBetweenWaves;

    [SerializeField] private IntVariable m_CurrentWave;
    [SerializeField] private IntVariable m_TotalWaves;

    public readonly List<Enemy> Enemies = new List<Enemy>();
    private void OnEnable()
    {
        GameEvents.OnEnemySpawned += AddEnemy;
        GameEvents.OnEnemyKilled += RemoveEnemy;
        StartCoroutine(SpawnEnemies());
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

    private IEnumerator SpawnEnemies()
    {
        m_TotalWaves.Value = m_Waves.Length;
        for (int waveIndex = 0; waveIndex < m_Waves.Length; waveIndex++)
        {
            m_CurrentWave.Value = waveIndex + 1;
            var wave = m_Waves[waveIndex];
            var spawnInterval = new WaitForSeconds(wave.SpawnInterval);
            var spawnPortionList = new List<EnemyTypeSO>();
            for (int i = 0; i < wave.Enemies.Count; i++)
            {
                for (int count = 0; count < wave.Enemies[i].Portion; count++)
                {
                    spawnPortionList.Add(wave.Enemies[i].EnemyType);
                }
            }

            for (float time = 0; time <= wave.Duration; time += wave.SpawnInterval)
            {
                var enemyType = spawnPortionList[Random.Range(0, spawnPortionList.Count)];
                SpawnEnemy(enemyType);
                yield return spawnInterval;
            }

            yield return new WaitForSeconds(TimeBetweenWaves);
        }
    }

    private void SpawnEnemy(EnemyTypeSO enemyType)
    {
        var enemy = Instantiate(enemyType.EnemyPrefab, Random.insideUnitSphere + transform.position, Quaternion.identity);
        enemy.SetTarget(m_Castle.transform.position);
    }
}
