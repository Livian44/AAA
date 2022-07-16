using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(CircleCollider2D))]
public class BasicTower : MonoBehaviour
{
    [field:SerializeField]
    public TowerType TowerType { get; set; }

    private float currentReloadTime = 0;
    private Enemy currentTarget;
    private CircleCollider2D collider;
    private List<Enemy> enemiesInRange = new List<Enemy>();
    private Vector3 lastShootingDirection = Vector3.zero;
    private Animator animator;
    private string animationBoolName = "IsShooting";
    

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.Log("Tower is missing animator!");
        }
        ConfigureCollider();
    }

    private void ConfigureCollider()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = TowerType.Range;
    }

    public void Update()
    {
        if (currentTarget == null)
        {
            animator.SetBool(animationBoolName,false);
            SearchForNewTarget();
            return;
        }
        currentReloadTime -= Time.deltaTime;
        if (currentReloadTime <= 0)
        {
            ResetSpawnTimer();
            AttackEnemy();
        }

        RotateTower();
    }

    private void RotateTower()
    {
        Vector3 direction = (currentTarget.transform.position-transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (enemy && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }

        if (currentTarget == null)
        {
            currentTarget = enemy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }

        if (enemy == currentTarget)
        {
            SearchForNewTarget();
        }
    }

    private void ResetSpawnTimer()
    {
        currentReloadTime = TowerType.ReloadTime;
    }
    private void SearchForNewTarget()
    {
        if (enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange.First();
        }
    }

    private void AttackEnemy()
    {
        animator.SetBool(animationBoolName,true);
       currentTarget.ReceiveDamage(TowerType.ProjectilePrefab.ProjectileType.Attack);
    }

    private void OnDrawGizmos()
    {
        if (currentTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,currentTarget.transform.position);
        }
        if (lastShootingDirection != Vector3.zero)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + lastShootingDirection);
        }
    }
}
