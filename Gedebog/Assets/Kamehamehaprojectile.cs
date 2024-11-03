using System.Collections;
using UnityEngine;

public class KamehamehaProjectile : MonoBehaviour
{
    private int damage;
    private float duration;
    private float elapsedTime = 0f;

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    public void SetDuration(float durationTime)
    {
        duration = durationTime;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            Destroy(gameObject); // Destroy projectile after the duration ends
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Ensure boss is tagged as "Boss"
        {
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                Debug.Log("Kamehameha hits boss for " + damage + " damage.");
            }
        }
    }
}
