using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Camera mainCamera;
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
        Tower.OnTowerSelected += HandleTowerSelected;
    }
    private void OnDisable()
    {
        TowerSpawnPoint.OnSpawnPointSelected -= HandleSpawnPointSelected;
        Tower.OnTowerSelected -= HandleTowerSelected;
    }

    // Turret Sell
    private void HandleTowerSelected(Tower turret, bool selected)
    {
        if (!SpawnedTowers.ContainsKey(turret)) return;

        if (selected)
        {
            var spawnPoint = SpawnedTowers[turret];
            var dialogPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
            sellDialog.Show(dialogPosition, () =>
            {
                spawnPoint.ClickEnabled = true;
                Destroy(turret.gameObject);
                SpawnedTowers.Remove(turret);
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
