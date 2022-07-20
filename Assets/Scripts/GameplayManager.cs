using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    public UnityEvent OnEnemyKilled;

    public UnityEvent OnGameEnd;
    
    private static GameplayManager instance;
    public static GameplayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameplayManager>();
            }
            return instance;
        }
        set => instance = value;
    }
    [SerializeField]
    private float value;

    [field:SerializeField]
    public Level Level { get; set; }
    [field:SerializeField]
    public PlayerController PlayerController { get; set; }

    private int playerHealth = 30;
    
    public void EnemyKilledByPlayer()
    {
        PlayerController.IncreasePlayerMoney(1);
        PlayerController.IncreasePlayerScore(10);
        OnEnemyKilled.Invoke();
    }
    
    public void EnemyReachedLevelEnd(EnemyType enemyType)
    {
        RemovePlayerHealth(enemyType.Attack);
    }

    private void RemovePlayerHealth(int value)
    {
        playerHealth -= value;
        if (playerHealth <= 0)
        {
            OnGameEnd?.Invoke();
        }
    }
}
