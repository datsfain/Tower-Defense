using System;
using UnityEngine;

public class TowerSellDialog : MonoBehaviour
{
    private Action OnSubmit;
    public void Show(Vector2 position, Action callback)
    {
        transform.position = position;
        OnSubmit = callback;
        gameObject.SetActive(true);
    }

    public void Submit()
    {
        Debug.Log("Submuit");
        OnSubmit?.Invoke();
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
