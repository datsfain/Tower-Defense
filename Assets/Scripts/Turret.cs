using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, ISelectable
{
    public static Action<Turret,bool> OnTurretSelected;

    public void OnDeselected()
    {
        OnTurretSelected?.Invoke(this, false);
        Debug.Log("Turret Deselected", this);
    }

    public void OnSelected()
    {
        OnTurretSelected?.Invoke(this, true);
        Debug.Log("Turret Selected", this);
    }
}
