using UnityEngine;

public class Tower : MonoBehaviour, ISelectable
{
    public TowerTypeSO TowerType { get; private set; }
    public void Initialize(TowerTypeSO towerType)
    {
        TowerType = towerType;
    }

    public void OnSelected() => TowerEvents.OnTowerSelected(this, true);
    public void OnDeselected() => TowerEvents.OnTowerSelected(this, false);
}
