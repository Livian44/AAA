using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [field:SerializeField]
    public List<Enemy> EnemiesToSpawn { get; set; }

    [field: SerializeField] 
    public int NumberOfEnemies { get; set; } = 10;
    
    [field: SerializeField] 
    public float TimeBetweenEnemySpawns { get; set; } = 0.5f;

    [field:SerializeField]
    public List<Transform> LevelPoints { get; set; }
    public Transform LevelStart => LevelPoints.First();
    public Transform LevelEnd => LevelPoints.Last();

    private float currentTimer;

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void ResetSpawnTimer()
    {
        currentTimer = TimeBetweenEnemySpawns;
    }

    public void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            ResetSpawnTimer();
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        Enemy selectedEnemy = EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Count)];
        Instantiate(selectedEnemy.gameObject, LevelStart.position,Quaternion.identity);
    }

    public Transform GetNextPoint(Transform previousPoint)
    {
        int indexOf = LevelPoints.IndexOf(previousPoint);
        
        if (previousPoint == LevelEnd)
        {
            return null;
        }

        return LevelPoints[indexOf + 1];
    }
}
