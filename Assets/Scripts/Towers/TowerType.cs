using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerType", menuName = "TowerType")]
public class TowerType : ScriptableObject
{
    [field:SerializeField]
    public Sprite Sprite { get; set; }
    
    [field: SerializeField] 
    public Projectile ProjectilePrefab { get; set; }

    [field: SerializeField] 
    public float Range { get; set; } = 1;

    [field: SerializeField,Range(0.1f,5f)] 
    public float ReloadTime { get; set; } = 1;

    [field: SerializeField] 
    public int Price { get; set; } = 1;
}
