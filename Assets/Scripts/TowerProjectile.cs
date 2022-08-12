using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Tower m_Tower;
    private Enemy m_Enemy;

    [SerializeField] private float m_Speed;
    [SerializeField] private Rigidbody m_Rigidbody;

    public void Initialize(Tower tower, Enemy enemy)
    {
        m_Tower = tower;
        m_Enemy = enemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if(enemy == m_Enemy)
        {
            GameEvents.OnTowerDamageEnemy?.Invoke(m_Tower, m_Enemy);
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, m_Enemy.transform.position);
    }

    private void Update()
    {
        if(m_Enemy == null)
        {
            Destroy(gameObject);
            return;
        }
        var deltaPosition = m_Enemy.transform.position - transform.position;
        m_Rigidbody.velocity = deltaPosition.normalized * m_Speed;
    }
}
