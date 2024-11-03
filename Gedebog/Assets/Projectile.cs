using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public int damage = 10; // Damage dealt by the projectile
    public float lifetime = 2f; // Time before the projectile is destroyed

    [HideInInspector] public Vector2 direction = Vector2.right; // Direction of the projectile (default is right)

    void Start()
    {
        // Destroy the projectile after a certain time to avoid clutter
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile collided with: " + collision.name); // Debug to check collision

        if (collision.CompareTag("Enemy")) // Make sure the boss has the "Enemy" tag
        {
            Debug.Log("Projectile hit an enemy!");

            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                Debug.Log("Applying damage to boss.");
                bossHealth.TakeDamage(damage); // Apply damage to the boss
            }
            else
            {
                Debug.Log("BossHealth component not found on enemy!");
            }
            
            Destroy(gameObject); // Destroy the projectile upon hitting an enemy
        }
        else
        {
            Debug.Log("Collision not with Enemy tag. No damage applied.");
        }
    }
}
