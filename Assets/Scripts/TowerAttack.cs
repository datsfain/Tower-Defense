using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private Tower m_Tower;
    [SerializeField] private TowerProjectile m_Projectile;
    [SerializeField] private Transform m_ShootPosition;

    private TowerTypeSO Stats => m_Tower.TowerType;
    private Transform m_Castle;
    private EnemyManager m_EnemyManager;
    private float m_TimeSinceLastAttack;
    private float m_SqrRange;

    private void Awake()
    {
        var attackRange = Stats.AttackRange;
        m_SqrRange = attackRange * attackRange;
    }

    public void Initialize(Transform castle, EnemyManager enemyManager)
    {
        m_Castle = castle;
        m_EnemyManager = enemyManager;
    }

    private void Update()
    {
        m_TimeSinceLastAttack += Time.deltaTime;
        if (m_TimeSinceLastAttack >= Stats.ShootInterval)
        {
            Shoot();
        }
    }

    private float GetSqrDistanceFromCastle(Enemy enemy)
    {
        return (m_Castle.transform.position - enemy.transform.position).sqrMagnitude;
    }

    private bool InAttackRange(Enemy enemy)
    {
        return (transform.position - enemy.transform.position).sqrMagnitude <= m_SqrRange;
    }

    private void Shoot()
    {
        var enemies = m_EnemyManager.Enemies;

        Enemy target = null;
        var minDistance = float.MaxValue;

        foreach(var enemy in enemies)
        {
            var distanceFromCastle = GetSqrDistanceFromCastle(enemy);
            if (InAttackRange(enemy) && distanceFromCastle < minDistance)
            {
                target = enemy;
                minDistance = distanceFromCastle;
            }
        }

        if(target != null)
        {
            m_TimeSinceLastAttack = 0f;
            var projectile = Instantiate(m_Projectile, m_ShootPosition.position, Quaternion.identity);
            projectile.Initialize(m_Tower, target);
        }
    }
}
