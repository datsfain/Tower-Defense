using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private NavMeshAgent m_Agent;

    private bool m_HasReachedTarget
    {
        get
        {
            return m_Agent.velocity.sqrMagnitude <= 0.01f;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetNewDestination();
        }

        var walking = !m_HasReachedTarget;
        m_Animator.SetBool("Walking", walking);
    }

    private void Hit()
    {

    }

    private void SetNewDestination()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            m_Agent.SetDestination(hit.point);
        }
    }
}
