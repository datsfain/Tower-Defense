using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEventSO GameEvent;
    public UnityEvent Response;
    public void OnEventRaised() => Response.Invoke();
    private void OnEnable() => GameEvent.AddListener(OnEventRaised);
    private void OnDisable() => GameEvent.RemoveListener(OnEventRaised);
}
