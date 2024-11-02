using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuard : MonoBehaviour
{
    public bool isGuarding = false;                // Tracks if the player is currently guarding
    public float guardDamageMultiplier = 0.5f;     // Reduces damage by half when guarding
    private PlayerHealth playerHealth;             // Reference to the PlayerHealth component

    void Start()
    {
        // Get reference to the PlayerHealth script
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Check if the F key is held down to activate guarding
        isGuarding = Input.GetKey(KeyCode.F);

        // Test taking damage with the '8' key while guarding
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            TakeDamage(1, Vector2.left); // Test taking 1 damage with knockback to the left
        }
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        // Apply damage with guard reduction if the player is guarding
        float finalDamage = isGuarding ? damage * guardDamageMultiplier : damage;
        playerHealth.TakeDamage(finalDamage, knockbackDirection); // Pass damage and knockback direction to PlayerHealth
    }
}
