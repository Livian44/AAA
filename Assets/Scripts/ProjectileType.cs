using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "ProjectileType")]
public class ProjectileType : ScriptableObject
{
    [field:SerializeField]
    public Sprite Sprite { get; set; }

    [field: SerializeField] 
    public int Attack { get; set; } = 1;

    [field: SerializeField] 
    public int Speed { get; set; } = 1;
}
