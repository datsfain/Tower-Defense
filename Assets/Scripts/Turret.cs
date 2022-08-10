using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, IPointerClickHandler
{
    public static Action<Turret> OnTurretClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTurretClicked?.Invoke(this);
        Debug.Log("Turret Clicked", this);
    }
}
