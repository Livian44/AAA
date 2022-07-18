using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType")]
public class EnemyType : ScriptableObject
{
    [field:SerializeField]
    public Sprite Sprite { get; set; }

    [field: SerializeField] 
    public int Attack { get; set; } = 1;

    [field: SerializeField] 
    public int Health { get; set; } = 3;

    [field: SerializeField] 
    public float Speed { get; set; } = 1;
}
