using UnityEngine;

public class Tower : MonoBehaviour, ISelectable
{
    public TowerTypeSO TowerType { get; private set; }
    public void Initialize(TowerTypeSO towerType)
    {
        TowerType = towerType;
    }

    public void OnSelected() => GameEvents.OnTowerSelected(this, true);
    public void OnDeselected() => GameEvents.OnTowerSelected(this, false);
}
