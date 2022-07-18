using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(CircleCollider2D))]
public class ProjectileTower : MonoBehaviour , ITower
{
    [field:SerializeField]
    public TowerType TowerType { get; set; }

    public GameObject GameObject => gameObject;

    private float currentReloadTime = 0;
    private Enemy currentTarget;
    private CircleCollider2D collider;
    private List<Enemy> enemiesInRange = new List<Enemy>();
    Vector3 lastShootingDirection = Vector3.zero;
    
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = TowerType.Range;
    }

    public void Update()
    {
        if (currentTarget == null)
        {
            SearchForNewTarget();
            return;
        }
        currentReloadTime -= Time.deltaTime;
        if (currentReloadTime <= 0)
        {
            ResetSpawnTimer();
            SpawnProjectile();
        }
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

    private void SpawnProjectile()
    {
        Vector3 direction = (currentTarget.transform.position-transform.position).normalized;
        lastShootingDirection = direction;
        GameObject projectile = Instantiate(TowerType.ProjectilePrefab.gameObject, transform.position,Quaternion.identity);
        direction.z = 0;
        
        float towerAngle = Vector3.SignedAngle(transform.up, direction.normalized,Vector3.back);
        transform.Rotate(Vector3.back,towerAngle);
        
        float projectileAngle = Vector3.SignedAngle(projectile.transform.up, direction.normalized,Vector3.back);
        projectile.transform.Rotate(Vector3.back,projectileAngle);
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
