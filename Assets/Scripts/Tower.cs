using UnityEngine;

public class Tower : MonoBehaviour, ISelectable
{
    [field: SerializeField] public TowerTypeSO TowerType { get; private set; }

    [SerializeField] private TowerAttack m_TowerAttack;

    public void Initialize(Transform castle)
    {
        m_TowerAttack.Initialize(castle);
    }

    public void OnSelected() => GameEvents.OnTowerSelected(this, true);
    public void OnDeselected() => GameEvents.OnTowerSelected(this, false);
}
