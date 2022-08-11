using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Type", menuName = "Scriptable Objects/TowerType")]
public class TowerTypeSO : ScriptableObject
{
    [field: SerializeField] 
    public Tower TowerPrefab { get; private set; }

    [field: SerializeField]
    public Sprite DisplaySprite { get; private set; }

    [field: SerializeField, Range(0f, 5f)]
    public float ShootInterval { get; private set; }

    [field: SerializeField, Range(0f, 20f)]
    public float AttackRange { get; private set; }


    [field: SerializeField]
    public int BuildPrice { get; private set; }


    [field: SerializeField]
    public int Damage { get; private set; }
}
