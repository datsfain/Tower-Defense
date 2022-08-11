using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public TurretSpawnDialog spawnDialog;
    public TurretSellDialog sellDialog;
    public Turret TurretPrefab;
    private Dictionary<Turret, TurretSpawnPoint> SpawnedTurrets;

    private void Awake()
    {
        SpawnedTurrets = new Dictionary<Turret, TurretSpawnPoint>();
    }
    private void OnEnable()
    {
        TurretSpawnPoint.OnSpawnPointSelected += HandleSpawnPointSelected;
        Turret.OnTurretSelected += HandleTurretSelected;
    }
    private void OnDisable()
    {
        TurretSpawnPoint.OnSpawnPointSelected -= HandleSpawnPointSelected;
        Turret.OnTurretSelected -= HandleTurretSelected;
    }

    // Turret Sell
    private void HandleTurretSelected(Turret turret, bool selected)
    {
        if (!SpawnedTurrets.ContainsKey(turret)) return;

        if (selected)
        {
            var spawnPoint = SpawnedTurrets[turret];
            var dialogPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
            sellDialog.Show(dialogPosition, () =>
            {
                spawnPoint.ClickEnabled = true;
                Destroy(turret.gameObject);
                SpawnedTurrets.Remove(turret);
            });
        }
        else
        {
            sellDialog.Hide();
        }
    }


    // Turret Spawn
    private void SpawnTurret(TurretSpawnPoint spawnPoint, int index)
    {
        Debug.Log($"Spawning {index}");
        if (index != -1)
        {
            var turret = Instantiate(TurretPrefab, spawnPoint.transform.position, Quaternion.identity);
            spawnPoint.ClickEnabled = false;
            SpawnedTurrets.Add(turret, spawnPoint);
        }
    }
    private void HandleSpawnPointSelected(TurretSpawnPoint spawnPoint, bool selected)
    {
        var dialogScreenPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
        if (selected)
        {
            spawnDialog.Show(dialogScreenPosition, index => SpawnTurret(spawnPoint, index));
        }
        else
        {
            spawnDialog.Hide();
        }
    }
}
