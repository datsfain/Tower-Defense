using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSellDialog : MonoBehaviour
{
    private Action m_Callback;
    [SerializeField] private Image m_TowerIcon;
    [SerializeField] private TMP_Text m_SellAmountText;

    public void Show(Tower tower, Vector2 position, Action callback)
    {
        var towerType = tower.TowerType;
        m_TowerIcon.sprite = towerType.DisplaySprite;
        m_SellAmountText.text = towerType.BuildPrice.ToString();

        transform.position = position;
        m_Callback = callback;
        gameObject.SetActive(true);
    }

    public void Submit()
    {
        m_Callback?.Invoke();
        Hide();
    }

    public void Hide()
    {
        m_Callback = null;
        gameObject.SetActive(false);
    }
}
