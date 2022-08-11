using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEventsObserver : MonoBehaviour
{
    public IntVariable Gold;

    private void Awake()
    {
        Gold.Value = 300;
    }

    public void OnEnable()
    {
        TowerEvents.OnTowerBuilt += HandleTowerBuild;
        TowerEvents.OnTowerSold += HandleTowerSold;
    }

    public void OnDisable()
    {
        TowerEvents.OnTowerBuilt -= HandleTowerBuild;
        TowerEvents.OnTowerSold -= HandleTowerSold;
    }

    private void HandleTowerSold(Tower tower)
    {
        Debug.Log("Sold");
        Debug.Log(Gold.Value);
        Gold.Value += tower.TowerType.BuildPrice;
        Debug.Log(Gold.Value);
    }

    private void HandleTowerBuild(Tower tower)
    {
        Debug.Log("Building");
        Gold.Value -= tower.TowerType.BuildPrice;
    }
}
