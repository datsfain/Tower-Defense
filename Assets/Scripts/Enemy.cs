using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyTypeSO Stats { get; private set; }
    [SerializeField] private Animator m_Animator;
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private float m_AttackDamageDelay;
    [SerializeField] private Canvas m_Canvas;

    private Camera m_Camera;

    private bool Walking => m_Agent.velocity.sqrMagnitude > 0.1f;
    private WaitForSeconds m_AttackDelay;
    private float m_TimeSinceLastAttack = 1f;

    private static readonly int k_WalkingAnimationHash = Animator.StringToHash("Walking");
    private static readonly int k_AttackAnimationHash = Animator.StringToHash("Attack");

    private bool HasReachedTarget
    {
        get
        {
            var deltaPosition = transform.position - m_Agent.destination;
            var closeEnough = deltaPosition.magnitude <= m_Agent.stoppingDistance + Stats.AttackRange;
            return !Walking && closeEnough;
        }
    }

    public void SetTarget(Vector3 destination)
    {
        m_Agent.destination = destination;
    }

    private void Awake()
    {
        m_AttackDelay = new WaitForSeconds(m_AttackDamageDelay);
        m_TimeSinceLastAttack = Stats.AttackInterval;
        m_Agent.speed = Stats.MoveSpeed;
        m_Camera = Camera.main;
    }

    private void OnEnable()
    {
        GameEvents.OnEnemySpawned?.Invoke(this);
    }
    private void OnDisable()
    {
        GameEvents.OnEnemyKilled?.Invoke(this);
    }

    private void Update()
    {
        m_Canvas.transform.forward = m_Camera.transform.forward;

        if (Input.GetMouseButtonDown(1))
        {
            SetNewDestination();
        }

        UpadateAttackState();

        m_Animator.SetBool(k_WalkingAnimationHash, Walking);
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
        m_Animator.SetTrigger(k_AttackAnimationHash);
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
