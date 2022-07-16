using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [field:SerializeField]
    public EnemyType EnemyType { get; set; }

    private int health;
    private Transform lastPoint;
    private Transform nextPoint;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    
    void Start()
    {
        ConfigureRigidbody();
        ConfigureRenderer();
        ConfigureCollider();
        ConfigureStats();
        ConfigureMovement();
    }

    private void ConfigureRigidbody()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    private void ConfigureMovement()
    {
        lastPoint = GameplayManager.Instance.Level.LevelStart;
        CalculateNextPoint();
    }

    private void ConfigureStats()
    {
        health = EnemyType.Health;
    }

    private void ConfigureRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = EnemyType.Sprite;
    }

    private void ConfigureCollider()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = false;
        collider.size = spriteRenderer.size;
    }

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
