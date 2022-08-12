using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    [SerializeField] private IntVariable m_CurrentHealth;
    [SerializeField] private IntVariable m_MaxHealth;
    private void OnEnable()
    {
        m_CurrentHealth.OnValueChanged += SetSliderValue;
        SetSliderValue();
    }
    private void OnDisable()
    {
        m_CurrentHealth.OnValueChanged -= SetSliderValue;
    }
    private void SetSliderValue()
    {
        m_Slider.value = (float)m_CurrentHealth.Value / m_MaxHealth.Value;
    }
}
