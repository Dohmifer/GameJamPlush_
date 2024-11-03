using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    public int damagePerHit = 5;
    public float delayBetweenHits = 0.5f;
    public float knockbackForce = 5f;
    public float attackRange = 1f;
    public Vector2 attackOffset = new Vector2(1f, 0); // Offset for the attack range in front of the player

    public LayerMask enemyLayer; // Layer mask for the enemy layer

    private bool isAttacking = false;
    private HashSet<Collider2D> enemiesHit; // Track enemies hit per attack sequence

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetBool("Attack", true);
        enemiesHit = new HashSet<Collider2D>(); // Reset the hit tracker each time an attack starts
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        // First hit with knockback
        ApplyDamage();

        // Wait for delay
        yield return new WaitForSeconds(delayBetweenHits);

        // Second hit with knockback
        ApplyDamage();

        // Reset attack trigger and allow attacking again
        animator.SetBool("Attack", false);
        isAttacking = false;
    }

    public void ApplyDamage()
    {
        Debug.Log("Dealing " + damagePerHit + " damage");

        Vector2 attackPosition = (Vector2)transform.position + attackOffset * transform.localScale.x;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Check if the enemy was already hit in this attack sequence
            if (!enemiesHit.Contains(enemy))
            {
                Debug.Log("Hit enemy: " + enemy.name);
                
                BossHealth bossHealth = enemy.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(damagePerHit); // Apply 5 damage per hit
                }

                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }

                // Add the enemy to the list of hit enemies to prevent re-damaging
                enemiesHit.Add(enemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Offset position to draw the attack range in front of the player
        Vector2 gizmoPosition = (Vector2)transform.position + attackOffset * transform.localScale.x;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmoPosition, attackRange);
    }
}
