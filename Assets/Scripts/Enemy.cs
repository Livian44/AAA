using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [field:SerializeField]
    public EnemyType EnemyType { get; set; }

    private SpriteRenderer spriteRenderer;
    private int health;
    private Transform lastPoint;
    private Transform nextPoint;
    
    void Update()
    {
        Vector3 targetDirection = (nextPoint.position - transform.position).normalized;
        float speed = EnemyType.Speed * Time.deltaTime;
        transform.Translate(targetDirection*speed);
        if (Vector3.Distance(transform.position, nextPoint.position) < 0.1f)
        {
            CalculateNextPoint();
        }
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = EnemyType.Sprite;
        health = EnemyType.Health;
        lastPoint = GameplayManager.Instance.Level.LevelStart;
        CalculateNextPoint();
    }

    private void CalculateNextPoint()
    {
        lastPoint = nextPoint;
        nextPoint = GetNextPoint();
        if (nextPoint == null)
        {
            Debug.Log("Reached end of the level!");
            GameplayManager.Instance.EnemyReachedLevelEnd(EnemyType);
            KillEnemy();
        }
        
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            KillEnemy();
        }
    }
    
    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    private Transform GetNextPoint()
    {
        return GameplayManager.Instance.Level.GetNextPoint(nextPoint);
    }
    
}
