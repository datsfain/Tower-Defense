using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : MonoBehaviour
{
    private TowerTypeSO m_TowerType;
    private TowerSpawnDialog m_Dialog;
    [SerializeField] private Image m_DisplayImage;

    public void Initialize(TowerSpawnDialog dialog, TowerTypeSO towerType)
    {
        m_TowerType = towerType;
        m_Dialog = dialog;
        m_DisplayImage.sprite = towerType.DisplaySprite;
    }

    public void Spawn()
    {
        m_Dialog.Submit(m_TowerType);
    }
}
