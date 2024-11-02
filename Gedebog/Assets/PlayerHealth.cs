using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5f;                // Maximum health of the player
    private float currentHealth;                // Current health of the player
    public HealthBar healthBar;                 // Reference to the HealthBar UI component

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(Mathf.RoundToInt(maxHealth)); // Convert to int for health bar
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        // Apply damage
        currentHealth -= damage;

        // Update the health bar (converted to int)
        healthBar.SetHealth(Mathf.RoundToInt(currentHealth));
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // Apply knockback
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset any existing velocity
            rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse); // Adjust the knockback force as needed
        }

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
        // Add death behavior here (e.g., restart the level or display a game over screen)
    }
}
