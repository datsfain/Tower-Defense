using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public TurretSpawnDialog spawnDialog;
    public Turret TurretPrefab;
    private Dictionary<Turret, TurretSpawnPoint> SpawnedTurrets;

    private void Awake()
    {
        SpawnedTurrets = new Dictionary<Turret, TurretSpawnPoint>();
    }
    private void OnEnable()
    {
        TurretSpawnPoint.OnSpawnPointClicked += SpawnTurret;
        Turret.OnTurretClicked += RemoveTurret;
    }
    private void OnDisable()
    {
        TurretSpawnPoint.OnSpawnPointClicked -= SpawnTurret;
        Turret.OnTurretClicked -= RemoveTurret;
    }

    private void RemoveTurret(Turret turret)
    {
        if (!SpawnedTurrets.ContainsKey(turret)) return;

        SpawnedTurrets[turret].ClickEnabled = true;
        Destroy(turret.gameObject);
        SpawnedTurrets.Remove(turret);
    }

    private void SpawnTurret(TurretSpawnPoint spawnPoint)
    {
        var dialogScreenPosition = mainCamera.WorldToScreenPoint(spawnPoint.SpawnPosition);
        spawnDialog.Show(dialogScreenPosition, (int index) =>
        {
            Debug.Log($"Spawning {index}");
            if (index != -1)
            {
                var turret = Instantiate(TurretPrefab, spawnPoint.transform.position, Quaternion.identity);
                spawnPoint.ClickEnabled = false;
                SpawnedTurrets.Add(turret, spawnPoint);
            }
        });
    }
}
