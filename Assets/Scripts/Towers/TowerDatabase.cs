using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "TowerDatabase", menuName = "TowerDatabase", order = 0)]
    public class TowerDatabase : ScriptableObject
    {
        [field: SerializeField] 
        public List<GameObject> Towers { get; set;}
    }
}