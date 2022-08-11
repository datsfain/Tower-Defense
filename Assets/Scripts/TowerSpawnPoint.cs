using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawnPoint : MonoBehaviour, ISelectable
{
    public static Action<TowerSpawnPoint, bool> OnSpawnPointSelected;
    [SerializeField] Collider m_Collider;
    [SerializeField] MeshRenderer m_Renderer;
    public bool _clickEnabled = true;
    public bool ClickEnabled
    {
        set
        {
            _clickEnabled = value;
            m_Collider.enabled = _clickEnabled;
            m_Renderer.enabled = _clickEnabled;
        }
    }
    public Vector3 SpawnPosition => new Vector3(transform.position.x, 0f, transform.position.z);

    public void OnSelected()
    {
        OnSpawnPointSelected?.Invoke(this, true);
    }

    public void OnDeselected()
    {
        OnSpawnPointSelected?.Invoke(this, false);
    }
}
