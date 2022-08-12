using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<Tower> OnTowerBuilt;
    public static Action<Tower> OnTowerSold;
    public static Action<Tower, bool> OnTowerSelected;
    public static Action<Enemy> OnEnemyDamageCastle;
    public static Action<Tower, Enemy> OnTowerDamageEnemy;
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemySpawned;
}

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_Castle;
    [SerializeField] private TowerSpawnDialog m_SpawnDialog;
    [SerializeField] private TowerSellDialog m_SellDialog;
    [SerializeField] private EnemyTracker m_EnemyTracker;
    private Dictionary<Tower, TowerSpawnPoint> m_SpawnedTowers;

    private void Awake()
    {
        m_SpawnedTowers = new Dictionary<Tower, TowerSpawnPoint>();
    }
    private void OnEnable()
    {
        TowerSpawnPoint.OnSpawnPointSelected += HandleSpawnPointSelected;
        GameEvents.OnTowerSelected += HandleTowerSelected;
    }
    private void OnDisable()
    {
        TowerSpawnPoint.OnSpawnPointSelected -= HandleSpawnPointSelected;
        GameEvents.OnTowerSelected -= HandleTowerSelected;
    }

    // Turret Sell
    private void HandleTowerSelected(Tower tower, bool selected)
    {
        if (!m_SpawnedTowers.ContainsKey(tower)) return;

        if (selected)
        {
            var spawnPoint = m_SpawnedTowers[tower];
            var dialogPosition = m_Camera.WorldToScreenPoint(spawnPoint.SpawnPosition);
            m_SellDialog.Show(tower, dialogPosition, () =>
            {
                GameEvents.OnTowerSold(tower);
                m_SpawnedTowers.Remove(tower);
                Destroy(tower.gameObject);
                spawnPoint.ClickEnabled = true;
            });
        }
        else
        {
            m_SellDialog.Hide();
        }
    }


    // Turret Spawn
    private void SpawnTower(TowerSpawnPoint spawnPoint, TowerTypeSO towerType)
    {
        var tower = Instantiate(towerType.TowerPrefab, spawnPoint.SpawnPosition, Quaternion.identity);
        tower.Initialize(m_Castle, m_EnemyTracker);

        GameEvents.OnTowerBuilt(tower);

        spawnPoint.ClickEnabled = false;
        m_SpawnedTowers.Add(tower, spawnPoint);
    }

    private void HandleSpawnPointSelected(TowerSpawnPoint spawnPoint, bool selected)
    {
        var dialogScreenPosition = m_Camera.WorldToScreenPoint(spawnPoint.SpawnPosition);
        if (selected)
        {
            m_SpawnDialog.Show(dialogScreenPosition, towerType => SpawnTower(spawnPoint, towerType));
        }
        else
        {
            m_SpawnDialog.Hide();
        }
    }
}
