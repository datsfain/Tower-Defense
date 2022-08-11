using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : MonoBehaviour
{
    public TowerTypeSO TowerType { get; private set; }
    private TowerSpawnDialog m_Dialog;
    [SerializeField] private Button m_Button;
    [SerializeField] private Image m_Background;
    [SerializeField] private Color m_EnabledColor;
    [SerializeField] private Color m_DisabledColor;
    [SerializeField] private float m_EnabledIconAlpha;
    [SerializeField] private float m_DisabledIconAlpha;

    [SerializeField] private Image m_DisplayImage;
    [SerializeField] private TMP_Text m_PriceText;

    public bool Enabled
    {
        set
        {
            m_DisplayImage.color = new Color(1f, 1f, 1f, value ? m_EnabledIconAlpha : m_DisabledIconAlpha);
            m_Button.enabled = value;
            m_Background.color = value ? m_EnabledColor : m_DisabledColor;
        }
    }

    public void Initialize(TowerSpawnDialog dialog, TowerTypeSO towerType, bool enabled)
    {
        Enabled = enabled;
        TowerType = towerType;
        m_Dialog = dialog;
        m_DisplayImage.sprite = towerType.DisplaySprite;
        m_PriceText.text = towerType.BuildPrice.ToString();
    }

    public void Spawn()
    {
        m_Dialog.Submit(TowerType);
    }
}
