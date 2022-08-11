using UnityEngine;

public interface ISelectable
{
    void OnSelected();
    void OnDeselected();
}

public class SelectionManager : MonoBehaviour
{
    private Collider current;
    private ISelectable m_Selected;
    [SerializeField] private Camera m_Camera;
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            OnClickDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnClickUp();
        }
#else
        if(Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                OnClickDown();
            }
            else if(Input.touches[0].phase == TouchPhase.Ended)
            {
                OnClickUp();
            }
        }
#endif
    }

    private void OnClickDown()
    {
        Debug.Log("OnClickDown");
#if UNITY_EDITOR
        RaycastHit hit;
        var ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            current = hit.collider;
        }
#else
        RaycastHit hit;
        var ray = m_Camera.ScreenPointToRay(Input.touches[0].position);
        if (Physics.Raycast(ray, out hit))
        {
            current = hit.collider;
        }
#endif
    }

    private void OnClickUp()
    {
        Debug.Log("OnClickUp");
#if UNITY_EDITOR
        RaycastHit hit;
        var ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == current)
            {
                var newSelected = hit.collider.GetComponent<ISelectable>();
                if (newSelected == m_Selected) return;
                m_Selected?.OnDeselected();
                m_Selected = newSelected;
                m_Selected?.OnSelected();
            }
        }
#else
        RaycastHit hit;
        var ray = m_Camera.ScreenPointToRay(Input.touches[0].position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == current)
            {
                var newSelected = hit.collider.GetComponent<ISelectable>();
                if (newSelected == m_Selected) return;
                m_Selected?.OnDeselected();
                m_Selected = newSelected;
                m_Selected?.OnSelected();
            }
        }
        
#endif

    }
}
