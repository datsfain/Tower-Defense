using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private float m_AttackDamageDelay;

    public EnemyTypeSO Stats { get; private set; }
    private float m_TimeSinceLastAttack = 1f;
    private WaitForSeconds m_AttackDelay;

    private bool Walking => m_Agent.velocity.sqrMagnitude > 0.1f;

    private bool HasReachedTarget
    {
        get
        {
            var deltaPosition = transform.position - m_Agent.destination;
            var closeEnough = deltaPosition.magnitude <= m_Agent.stoppingDistance + Stats.AttackRange;
            return !Walking && closeEnough;
        }
    }

    public void Initialize(EnemyTypeSO enemyType, Vector3 destination)
    {
        Stats = enemyType;
        m_Agent.destination = destination;
        m_AttackDelay = new WaitForSeconds(m_AttackDamageDelay);
        m_TimeSinceLastAttack = Stats.AttackInterval;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetNewDestination();
        }

        UpadateAttackState();

        m_Animator.SetBool("Walking", Walking);
    }

    private void UpadateAttackState()
    {
        m_TimeSinceLastAttack += Time.deltaTime;

        if (!HasReachedTarget) return;
        if (m_TimeSinceLastAttack <= Stats.AttackInterval) return;

        m_TimeSinceLastAttack = 0f;
        AttackCastle();
    }
    private void AttackCastle()
    {
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
    {
        m_Animator.SetTrigger("Attack");
        yield return m_AttackDelay;
        ApplyDamage();
    }

    private void ApplyDamage()
    {
        GameEvents.OnEnemyDamageCastle?.Invoke(this);
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
