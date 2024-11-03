using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public BossHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetBossMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetBossHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss is dead!");
        // Additional code to handle boss death, like playing animations or destroying the object
        Destroy(gameObject);
    }
}
