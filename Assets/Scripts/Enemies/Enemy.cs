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
    private bool isFrozen;
    private bool isSlowed;
    
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.flipX = false;
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
        if (isFrozen)
        {
            return;
        }
        Vector3 targetDirection = (nextPoint.position - transform.position).normalized;
        float speed;
        if (isSlowed)
        {
            speed = EnemyType.Speed * Time.deltaTime * 0.2f;
        }
        else
        {
            speed = EnemyType.Speed * Time.deltaTime;
        }
            
        transform.Translate(targetDirection*speed,Space.World);
        if (Vector3.Distance(transform.position, nextPoint.position) < 0.1f)
        {
            CalculateNextPoint();
        }
    }
    private void TurnEnemy()
    {
        Vector3 targetDirection;
        Debug.Log(render);
        if (nextPoint.position.x < lastPoint.position.x)
        {
            render.flipX = false;
        }
        else
        {
            render.flipX = true;
        }
        targetDirection.z = 0;
        //float angle = Vector3.SignedAngle(transform.up, targetDirection.normalized,Vector3.back);
        //transform.Rotate(Vector3.back,angle);
    }

    private void CalculateNextPoint()
    {
        if (nextPoint != null)
        {
            lastPoint = nextPoint;
        }
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
    
    public void ReceiveDamage(int damage, int type)
    {
        health -= damage;
        if (type == 1)
        {
            isFrozen = true;
            Invoke("FreezEnemy", 0.5f);
        }
        if (type == 2)
        {
            isSlowed = true;
            Invoke("normalSpeed", 1f);
        }
        if (health <= 0)
        {
            GameplayManager.Instance.EnemyKilledByPlayer();
            KillEnemy();
        }
    }

    private void normalSpeed()
    {
        isSlowed = false;
    }
    private void FreezEnemy()
    {
        isFrozen = false;
        Vector3 speed;
        speed.x = 0;
        speed.y = 0;
        speed.z = 0;
        transform.Translate(speed, Space.World);
    }

    private void SlowEnemy()
    {

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
