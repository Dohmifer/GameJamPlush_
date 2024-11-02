using System.Collections;
using UnityEngine;

public class KamehamehaProjectile : MonoBehaviour
{
    private int damage = 5; // Damage dealt per second
    private float duration = 2f; // Duration of Kamehameha
    private float damageInterval = 1f; // Time interval between DoT applications

    // Method to set damage amount from another script
    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    // Method to set duration from another script
    public void SetDuration(float durationTime)
    {
        duration = durationTime;
    }

    private void Start()
    {
        StartCoroutine(DamageOverTime());
    }

    private IEnumerator DamageOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Set the size and position of the damage area (adjustable)
            Vector2 boxSize = new Vector2(3f, 1f); // Width and height of damage area
            Vector2 boxPosition = (Vector2)transform.position + new Vector2(1.5f, 0f); // Position in front of the player

            // Detect enemies within the defined box area
            Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(boxPosition, boxSize, 0f);
            foreach (Collider2D enemy in enemiesHit)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    // Apply damage to the enemy
                    Debug.Log("Kamehameha hits enemy for " + damage + " damage.");
                    // If the enemy has a health component, reduce its health here
                    // Example: enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }

            elapsedTime += damageInterval;
            yield return new WaitForSeconds(damageInterval); // Wait for next damage tick
        }
    }

    // Visualization for the damage area in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set Gizmo color

        // Set the size and position of the Gizmo for visualization (matches damage area)
        Vector2 boxSize = new Vector2(8f, 3f); // Adjust size for visualization
        Vector2 boxPosition = (Vector2)transform.position + new Vector2(4f, 0f); // Adjust position

        // Draw a wireframe box to show the damage area in the editor
        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
}
