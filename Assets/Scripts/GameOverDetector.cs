using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDetector : MonoBehaviour
{
    [SerializeField] private IntVariable CastleHealth;
    [SerializeField] private EnemyManager EnemyManager;
    [SerializeField] private GameEventSO GameOverEvent;

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += CheckGameOver;
        CastleHealth.OnValueChanged += CheckGameOver;
    }

    private void CheckGameOver()
    {
        var gameOver = EnemyManager.AllEnemiesSpawned && EnemyManager.Enemies.Count <= 0 || CastleHealth.Value <= 0;
        if (gameOver) GameOverEvent.RaiseEvent();
    }
    private void CheckGameOver(Enemy obj)
    {
        CheckGameOver();
    }
}
