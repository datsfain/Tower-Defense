using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] private EnemyHealth m_EnemyHealth;
    [SerializeField] private Slider m_Slider;

    private void OnEnable()
    {
        m_EnemyHealth.OnValueChanged += DisplayHealth;
        DisplayHealth();
    }

    private void OnDisable()
    {
        m_EnemyHealth.OnValueChanged -= DisplayHealth;
    }

    private void DisplayHealth()
    {
        m_Slider.value = (float)m_EnemyHealth.CurrentHealth / m_EnemyHealth.MaxHealth;
    }
}
