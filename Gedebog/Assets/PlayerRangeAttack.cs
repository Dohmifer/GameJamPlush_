using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    public Transform shootPoint; // Assign the shoot point in the Inspector

    private void Update()
    {
        // Check for input to shoot using the 'O' key
        if (Input.GetKeyDown(KeyCode.O)) // O key
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Flip the projectile's direction if the player is facing left
        if (transform.localScale.x < 0)
        {
            Vector3 scale = projectile.transform.localScale;
            scale.x *= -1; // Flip the projectile horizontally
            projectile.transform.localScale = scale;
        }
    }
}
