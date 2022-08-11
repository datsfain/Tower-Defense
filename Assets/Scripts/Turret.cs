using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, ISelectable
{
    public static Action<Turret> OnTurretClicked;

    public void OnDeselected()
    {
        Debug.Log("Turret Deselected", this);
    }

    public void OnSelected()
    {
        Debug.Log("Turret Selected", this);
    }
}
