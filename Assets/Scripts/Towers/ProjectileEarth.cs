using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
    public class ProjectileEarth : MonoBehaviour
    {
        [field: SerializeField] 
        public ProjectileType ProjectileType { get; set; }
        
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D collider2D;
        private Rigidbody2D rigidBody2D;
        
        private void Start()
        {
            ConfigureSpriteRenderer();
            ConfigureCollider();
        }
        private void Update()
        {
            Vector3 projectileTranslation = transform.up*ProjectileType.Speed*Time.deltaTime;
            transform.Translate(projectileTranslation,Space.World);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.ReceiveDamage(ProjectileType.Attack, 2);
            }
            Destroy(gameObject);
        }

        private void ConfigureSpriteRenderer()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = ProjectileType.Sprite;
        }

        private void ConfigureCollider()
        {
            collider2D = GetComponent<BoxCollider2D>();
            collider2D.isTrigger = true;
            collider2D.size = spriteRenderer.size;
        }
    }
}