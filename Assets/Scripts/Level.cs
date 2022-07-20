using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [field:SerializeField]
    public List< EnemyWave> EnemyWaves { get; set; }
    [field:SerializeField]
    public TowerDatabase AvaliableTowers { get; set; }
    [field:SerializeField]
    public List<Transform> LevelPoints { get; set; }
    public Transform LevelStart => LevelPoints.First();
    public Transform LevelEnd => LevelPoints.Last();

    [field:SerializeField]
    public Tilemap TurretTilemap { get; set; }
    
    [SerializeField] 
    private Transform pathParent;
    private float currentSpawnTimer;
    private EnemyWave currentWave;
    private float timeBetweenWaves = 5;
    private float currentWaveTimer;

    private void Start()
    {
        Debug.Log("Lukasz start");
        
        currentWaveTimer = timeBetweenWaves;
        currentWave = EnemyWaves.First();
        ResetSpawnTimer();
    }

    private void ResetSpawnTimer()
    {
        currentSpawnTimer = currentWave.TimeBetweenEnemySpawns;
    }

    public void Update()
    {
        if (currentWave.NumberOfEnemies <= 0)
        {
            currentWaveTimer -= Time.deltaTime;
            if (currentWaveTimer <= 0)
            {
                StartNextWave();
            }
            return;
        }
        currentSpawnTimer -= Time.deltaTime;
        if (currentSpawnTimer <= 0)
        {
            ResetSpawnTimer();
            SpawnRandomEnemy();
        }
    }

    private void StartNextWave()
    {
        EnemyWaves.Remove(currentWave);
        if (EnemyWaves.Count > 0)
        {
            currentWave = EnemyWaves.First();
        }

        currentWaveTimer = timeBetweenWaves;
    }

    private void SpawnRandomEnemy()
    {
        Enemy selectedEnemy = currentWave.EnemiesToSpawn[Random.Range(0, currentWave.EnemiesToSpawn.Count)];
        Instantiate(selectedEnemy.gameObject, LevelStart.position,Quaternion.identity);
        currentWave.NumberOfEnemies--;
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

    [ContextMenu("Load Path Points")]
    private void LoadPathPoints()
    {
        LevelPoints.Clear();
        foreach (Transform child in pathParent)
        {
            LevelPoints.Add(child);
        }
    }
}
