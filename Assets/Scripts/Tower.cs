using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, ISelectable
{
    public static Action<Tower,bool> OnTowerSelected;

    public void OnDeselected()
    {
        OnTowerSelected?.Invoke(this, false);
    }

    public void OnSelected()
    {
        OnTowerSelected?.Invoke(this, true);
    }
}
