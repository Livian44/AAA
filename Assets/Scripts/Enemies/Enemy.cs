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
    private SpriteRenderer render;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    
    void Start()
    {
        ConfigureRigidbody();
        ConfigureRenderer();
        ConfigureCollider();
        ConfigureStats();
        ConfigureMovement();
        render = GetComponent<SpriteRenderer>();
        render.flipX = true;

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
        transform.Translate(targetDirection*speed,Space.World);
       if (Vector3.Distance(transform.position, nextPoint.position) < 0.1f)
        {
            CalculateNextPoint();
        }
    }
    private void TurnEnemy()
    {
        Vector3 targetDirection;
        if (nextPoint.position.x < lastPoint.position.x)
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }
        targetDirection.z = 0;
        //float angle = Vector3.SignedAngle(transform.up, targetDirection.normalized,Vector3.back);
        //transform.Rotate(Vector3.back,angle);
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
            return;
        }
        Vector3 targetDirection = (nextPoint.position - transform.position).normalized;
        targetDirection.z = 0;
        TurnEnemy();
    }
    
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameplayManager.Instance.EnemyKilledByPlayer();
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
