using UnityEngine;

public class TowerDamageApplier : MonoBehaviour
{
    [SerializeField] private IntVariable m_Coins;
    private void OnEnable()
    {
        GameEvents.OnTowerDamageEnemy += HandleTowerDamageEnemy;
        GameEvents.OnEnemyKilled += HandleEnemyKilled;
    }


    private void OnDisable()
    {
        GameEvents.OnTowerDamageEnemy -= HandleTowerDamageEnemy;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled(Enemy enemy)
    {
        m_Coins.Value += Random.Range(enemy.Stats.MinRewardCoins, enemy.Stats.MaxRewardCoins + 1);
    }

    private void HandleTowerDamageEnemy(Tower tower, Enemy enemy)
    {
        var damage = tower.TowerType.Damage;
        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.CurrentHealth -= damage;
    }
}
