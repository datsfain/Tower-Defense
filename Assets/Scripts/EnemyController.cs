using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Vector3 m_TargetPosition;
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_StopRangeSqrMgn;
    [SerializeField] private float m_MaxRotationDeltaDegrees;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetNewDestination();
        }

        GoToDestination();
    }

    private void GoToDestination()
    {
        var lookAtPosition = m_TargetPosition;
        lookAtPosition.y = transform.position.y;

        var deltaPosition = lookAtPosition - transform.position;
        var distance = deltaPosition.sqrMagnitude;

        if(distance < m_StopRangeSqrMgn)
        {
            m_Rigidbody.velocity = Vector3.zero;
            return;
        }

        var lookRotation = Quaternion.LookRotation(deltaPosition.normalized, Vector3.up);
        m_Rigidbody.rotation = Quaternion.RotateTowards(m_Rigidbody.rotation, lookRotation, m_MaxRotationDeltaDegrees);


        var moveDirection = transform.forward;
        m_Rigidbody.velocity = moveDirection * m_MoveSpeed;
    }

    private void SetNewDestination()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            m_TargetPosition = hit.point;
        }
    }
}
