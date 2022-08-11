using System;
using UnityEngine;

public class ScriptableVariable<T> : ScriptableObject
{
    public Action OnValueChanged;

    [SerializeField] private T _value;
    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            OnValueChanged?.Invoke();
        }
    }
}
