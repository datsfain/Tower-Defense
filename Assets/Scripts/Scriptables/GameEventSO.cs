using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Scriptable Objects/Events/Game Event")]
public class GameEventSO : ScriptableObject
{
    private event Action OnEventRaised;
    public void RaiseEvent() => OnEventRaised?.Invoke();
    public void AddListener(Action action) => OnEventRaised += action;
    public void RemoveListener(Action action) => OnEventRaised -= action;
}
