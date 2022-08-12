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
        GameEvents.OnTowerBuilt += HandleTowerBuild;
        GameEvents.OnTowerSold += HandleTowerSold;
    }

    public void OnDisable()
    {
        GameEvents.OnTowerBuilt -= HandleTowerBuild;
        GameEvents.OnTowerSold -= HandleTowerSold;
    }

    private void HandleTowerSold(Tower tower)
    {
        Coins.Value += tower.TowerType.BuildPrice;
    }

    private void HandleTowerBuild(Tower tower)
    {
        Coins.Value -= tower.TowerType.BuildPrice;
    }
}
