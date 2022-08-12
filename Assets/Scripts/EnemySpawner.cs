using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyWaveSO[] m_Waves;
    [SerializeField] private Transform m_Castle;
    [SerializeField] private float TimeBetweenWaves;

    [SerializeField] private IntVariable m_CurrentWave;
    [SerializeField] private IntVariable m_TotalWaves;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());
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
