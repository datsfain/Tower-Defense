using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private NavMeshAgent m_Agent;
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

        var walking = m_Agent.velocity.sqrMagnitude >= 0.01f;
        m_Animator.SetBool("Walking", walking);
    }

    private void SetNewDestination()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            m_TargetPosition = hit.point;
            m_Agent.SetDestination(m_TargetPosition);
        }
    }
}
