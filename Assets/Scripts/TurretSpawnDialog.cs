using System;
using UnityEngine;

public class TurretSpawnDialog : MonoBehaviour
{
    private Action<int> Callback;
    public void Show(Vector2 position, Action<int> callback)
    {
        transform.position = position;
        Callback = callback;
        gameObject.SetActive(true);
    }

    public void Submit(int turretIndex)
    {
        Callback?.Invoke(turretIndex);
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
