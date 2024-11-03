using UnityEngine;

public class PlayerRangeAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    public Transform shootPoint; // Assign the shoot point in the Inspector

    private bool isFacingRight = true; // Track the direction the player is facing

    private void Update()
    {
        // Check for input to shoot using the 'O' key
        if (Input.GetKeyDown(KeyCode.O)) // O key
        {
            Shoot();
        }

        // Example: Flip dengan tombol P untuk tes
        if (Input.GetKeyDown(KeyCode.P)) // P key untuk test Flip
        {
            Flip();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Set the direction based on player facing direction, default ke right jika facing right
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.direction = isFacingRight ? Vector2.right : Vector2.left;
        }
    }

    // Flip player direction dan juga shootPoint position
    private void Flip()
    {
        isFacingRight = !isFacingRight; // Toggle direction

        // Flip player scale di x-axis
        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;

        // Flip shootPoint position di x-axis agar ikut dengan player
        Vector3 shootPointPosition = shootPoint.localPosition;
        shootPointPosition.x= -1;
        shootPoint.localPosition = shootPointPosition;
    }
}