using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealthBar enemyHealthBar;
    public int maxEnemyHealth;
    public int currentEnemyHealth;
    private ItemDrop getItem;

    void Start()
    {
        //getItem = GetComponent<ItemDrop>();

        currentEnemyHealth = maxEnemyHealth;
        enemyHealthBar.SetEnemyMaxHealth(maxEnemyHealth);
    }

    public void EnemyTakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        enemyHealthBar.SetEnemyHealth(currentEnemyHealth);

        if (currentEnemyHealth <= 0)
        {
            EnemyDied();
        }
    }

    public void EnemyDied()
    {
        if (getItem != null)
        {
            getItem.Drop();
        }

        Destroy(gameObject);
    }
}
