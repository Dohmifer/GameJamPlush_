using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject bulletPrefab; // Referensi prefab bullet
    public Transform firePoint; // Titik tempat bullet muncul
    public float bulletForce; // Kekuatan bullet

    private bool facingRight = true;
    private AudioSource enemyAudio; // Sumber audio untuk suara attack

    void Awake()
    {
        enemyAudio = GetComponent<AudioSource>();
    }

    public void SetDirection(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }

    // Method ini akan dipanggil saat event Attack
    public void Attack()
    {
        if (enemyAudio != null)
        {
            enemyAudio.Play(); // Memutar audio serangan

            bulletForce = Mathf.Clamp(bulletForce, 0.5f, 5f);
        }

        // Instantiate bullet dengan arah berdasarkan facingRight
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        // Atur arah bullet
        if (facingRight)
        {
            rb2d.velocity = transform.right * bulletForce;
        }
        else
        {
            rb2d.velocity = transform.right * -bulletForce;
        }
    }

    // Method ini akan dipanggil untuk mengakhiri attack
    public void AttackTimeEnd()
    {
        // Atur ulang serangan ke status "tidak dapat menyerang" dan mulai timer ulang
        EnemyRangeAttack enemyScript = GetComponent<EnemyRangeAttack>();
        if (enemyScript != null)
        {
            enemyScript.canAttack = false;
            enemyScript.waitTime = enemyScript.startWaitTime;
        }
    }
}
