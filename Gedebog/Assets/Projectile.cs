using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public int damage = 10; // Damage dealt by the projectile
    public float lifetime = 2f; // Time before the projectile is destroyed

    void Start()
    {
        // Destroy the projectile after a certain time to avoid clutter
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile in the direction it is facing
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Placeholder for actual damage application
            Debug.Log("Hit enemy with damage: " + damage);
            Destroy(gameObject); // Destroy the projectile upon hitting an enemy
        }
    }
}
