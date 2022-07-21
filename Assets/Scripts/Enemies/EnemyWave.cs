using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EnemyWave
    {
        [field:SerializeField]
        public List<Enemy> EnemiesToSpawn { get; set; }

        [field: SerializeField] 
        public int NumberOfEnemies { get; set; } = 10;
    
        [field: SerializeField] 
        public float TimeBetweenEnemySpawns { get; set; } = 0.5f;
        public bool isEmpty()
        {
            return !(EnemiesToSpawn.Count > 0);
        }
    }
    
}