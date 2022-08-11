using System;
using UnityEngine;

public class TowerSpawnDialog : MonoBehaviour
{
    private Action<TowerTypeSO> m_Callback;
    [SerializeField] private TowerTypeSO[] m_TowerTypes;
    [SerializeField] private TowerSpawnButton m_ButtonPrefab;
    [SerializeField] private Transform m_ButtonsParent;

    private void Awake()
    {
        for (int i = 0; i < m_TowerTypes.Length; i++)
        {
            Instantiate(m_ButtonPrefab, m_ButtonsParent).Initialize(this, m_TowerTypes[i]);
        }
    }

    public void Show(Vector2 position, Action<TowerTypeSO> callback)
    {
        transform.position = position;
        m_Callback = callback;
        gameObject.SetActive(true);
    }

    public void Submit(TowerTypeSO towerType)
    {
        m_Callback?.Invoke(towerType);
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
