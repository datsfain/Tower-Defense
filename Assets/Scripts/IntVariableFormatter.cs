using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class IntVariableFormatter : MonoBehaviour
{
    public IntVariable[] Variables;
    public UnityEvent<string> OnValueChanged;
    public string FormatString;
    public void OnEnable()
    {
        foreach(var variable in Variables)
        {
            variable.OnValueChanged += InvokeValueChangeEvent;
        }
        InvokeValueChangeEvent();
    }

    public void OnDisable()
    {
        foreach (var variable in Variables)
        {
            variable.OnValueChanged -= InvokeValueChangeEvent;
        }
    }

    private void InvokeValueChangeEvent()
    {
        var args = Variables.Select(v => v.Value as object).ToArray();
        var formatted = string.Format(FormatString, args);
        OnValueChanged?.Invoke(formatted);
    }
}
