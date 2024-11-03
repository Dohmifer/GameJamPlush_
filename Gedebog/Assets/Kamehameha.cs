using System.Collections;
using UnityEngine;

public class Kamehameha : MonoBehaviour
{
    public GameObject kamehamehaPrefab;  // Reference to the Kamehameha projectile prefab
    public Transform shootPoint;         // Position from where the Kamehameha will be launched
    public float kamehamehaSpeed = 10f;  // Speed of the Kamehameha projectile
    public float duration = 2f;          // Duration for which the Kamehameha lasts
    public float cooldown = 10f;         // Cooldown time in seconds
    public int damage = 5;               // Damage dealt per second by the Kamehameha
    public MonoBehaviour playerMovement; // Reference to the player movement script
    public Animator animator;            // Reference to the Animator component

    private bool isFiring = false;       // To prevent firing multiple times while pressing X
    private float cooldownTimer = 0f;    // Tracks the cooldown time
    private bool facingRight = true;     // Tracks which direction the player is facing

    void Update()
    {
        // Flip the shootPoint if player direction changes
        if (Input.GetKeyDown(KeyCode.LeftArrow)) facingRight = false;
        if (Input.GetKeyDown(KeyCode.RightArrow)) facingRight = true;

        // Update shootPoint position to face the correct direction
        shootPoint.localPosition = facingRight ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);

        // Check if the cooldown has finished
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Decrease cooldown timer
        }

        // Trigger the ultimate skill with the X key if cooldown is finished
        if (Input.GetKeyDown(KeyCode.X) && !isFiring && cooldownTimer <= 0)
        {
            StartCoroutine(FireKamehameha());
            cooldownTimer = cooldown; // Reset the cooldown timer
        }
    }

    IEnumerator FireKamehameha()
    {
        isFiring = true;

        // Set the Animator parameters for the second skill
        if (animator != null)
        {
            animator.SetBool("SecondSkill", true);
            animator.SetBool("IsSecondSkill", true);
        }

        // Disable the player movement while firing the Kamehameha
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Instantiate the Kamehameha projectile at the shootPoint position
        GameObject kamehamehaInstance = Instantiate(kamehamehaPrefab, shootPoint.position, Quaternion.identity);

        // Set the Kamehameha direction based on the player's facing direction
        float direction = facingRight ? 1 : -1;

        // Get the Rigidbody2D component to apply movement to the projectile
        Rigidbody2D rb = kamehamehaInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(kamehamehaSpeed * direction, 0); // Adjust direction based on facingRight
        }

        // Assign damage to the KamehamehaProjectile component
        KamehamehaProjectile projectile = kamehamehaInstance.GetComponent<KamehamehaProjectile>();
        if (projectile != null)
        {
            projectile.SetDamage(damage);
            projectile.SetDuration(duration); // Pass the duration to control DoT
        }

        // Wait for the duration of the Kamehameha
        yield return new WaitForSeconds(duration);

        // Re-enable the player movement after firing the Kamehameha
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Reset the Animator parameters after finishing the skill
        if (animator != null)
        {
            animator.SetBool("SecondSkill", false);
            animator.SetBool("IsSecondSkill", false);
        }

        // Destroy the Kamehameha instance
        Destroy(kamehamehaInstance);

        isFiring = false;
    }
}
