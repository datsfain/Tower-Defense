using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEventsObserver : MonoBehaviour
{
    public IntVariable Coins;

    private void Awake()
    {
        Coins.Value = 300;
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
        Debug.Log(Coins.Value);
        Coins.Value += tower.TowerType.BuildPrice;
        Debug.Log(Coins.Value);
    }

    private void HandleTowerBuild(Tower tower)
    {
        Debug.Log("Building");
        Coins.Value -= tower.TowerType.BuildPrice;
    }
}
