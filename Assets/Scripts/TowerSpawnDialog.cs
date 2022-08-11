using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnDialog : MonoBehaviour
{
    private Action<TowerTypeSO> m_Callback;
    [SerializeField] private IntVariable m_Gold;
    [SerializeField] private TowerTypeSO[] m_TowerTypes;
    [SerializeField] private TowerSpawnButton m_ButtonPrefab;
    [SerializeField] private Transform m_ButtonsParent;

    private List<TowerSpawnButton> m_Buttons;

    private void Awake()
    {
        m_Buttons = new List<TowerSpawnButton>();
        for (int i = 0; i < m_TowerTypes.Length; i++)
        {
            var button = Instantiate(m_ButtonPrefab, m_ButtonsParent);
            button.Initialize(this, m_TowerTypes[i], false);
            m_Buttons.Add(button);
        }
    }

    private void OnEnable()
    {
        m_Gold.OnValueChanged += EnableButtons;
        EnableButtons();
    }
    private void OnDisable()
    {
        m_Gold.OnValueChanged -= EnableButtons;
    }

    private void EnableButtons()
    {
        Debug.Log("Enabling");
        m_Buttons.ForEach(button => button.Enabled = m_Gold.Value >= button.TowerType.BuildPrice);
    }

    public void Show(Vector2 position, Action<TowerTypeSO> callback)
    {
        transform.position = position;
        m_Callback = callback;
        gameObject.SetActive(true);
    }

    public void Submit(TowerTypeSO towerType)
    {
        if (m_Gold.Value >= towerType.BuildPrice)
        {
            m_Callback?.Invoke(towerType);
        }
        Hide();
    }

    public void Hide()
    {
        m_Callback = null;
        gameObject.SetActive(false);
    }
}
