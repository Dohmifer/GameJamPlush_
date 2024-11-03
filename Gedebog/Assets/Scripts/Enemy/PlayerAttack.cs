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

    private bool isAttacking = false;

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
        animator.SetTrigger("Attack");
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
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }

    public void ApplyDamage()
    {
        Debug.Log("Dealing " + damagePerHit + " damage");

        // Offset the attack range in front of the player
        Vector2 attackPosition = (Vector2)transform.position + attackOffset * transform.localScale.x;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + enemy.name);

                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
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
