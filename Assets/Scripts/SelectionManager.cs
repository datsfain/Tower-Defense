using UnityEngine;

public interface ISelectable
{
    void OnSelected();
    void OnDeselected();
}

public class SelectionManager : MonoBehaviour
{
    private ISelectable m_Selected;
    [SerializeField] private Camera m_Camera;
    private void Update()
    {
        CheckSelection();
    }

    private void SelectObjectWithScreenPosition(Vector3 screenPosition)
    {
        RaycastHit hit;
        var ray = m_Camera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var newSelected = hit.collider.GetComponent<ISelectable>();
                m_Selected?.OnDeselected();
                m_Selected = newSelected;
                m_Selected?.OnSelected();
            }
        }
    }

    private void CheckSelection()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            SelectObjectWithScreenPosition(Input.mousePosition);
        }
#else
        if(Input.touchCount > 0)
        {
            var touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
            {
                SelectObjectWithScreenPosition(touch.position);
            }
        }
#endif
    }
}
