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
    public static Action<EnemyTypeSO> OnEnemyKilled;
}

public class TowerSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public Transform Castle;
    public TowerSpawnDialog spawnDialog;
    public TowerSellDialog sellDialog;
    private Dictionary<Tower, TowerSpawnPoint> SpawnedTowers;

    private void Awake()
    {
        SpawnedTowers = new Dictionary<Tower, TowerSpawnPoint>();
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
        if (!SpawnedTowers.ContainsKey(tower)) return;

        if (selected)
        {
            var spawnPoint = SpawnedTowers[tower];
            var dialogPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
            sellDialog.Show(tower, dialogPosition, () =>
            {
                GameEvents.OnTowerSold(tower);
                SpawnedTowers.Remove(tower);
                Destroy(tower.gameObject);
                spawnPoint.ClickEnabled = true;
            });
        }
        else
        {
            sellDialog.Hide();
        }
    }


    // Turret Spawn
    private void SpawnTower(TowerSpawnPoint spawnPoint, TowerTypeSO towerType)
    {
        var tower = Instantiate(towerType.TowerPrefab, spawnPoint.SpawnPosition, Quaternion.identity);
        tower.Initialize(Castle);

        GameEvents.OnTowerBuilt(tower);

        spawnPoint.ClickEnabled = false;
        SpawnedTowers.Add(tower, spawnPoint);
    }
    private void HandleSpawnPointSelected(TowerSpawnPoint spawnPoint, bool selected)
    {
        var dialogScreenPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
        if (selected)
        {
            spawnDialog.Show(dialogScreenPosition, towerType => SpawnTower(spawnPoint, towerType));
        }
        else
        {
            spawnDialog.Hide();
        }
    }
}
