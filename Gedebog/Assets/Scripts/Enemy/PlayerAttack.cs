using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    public int damagePerHit = 5;
    public float delayBetweenHits = 0.5f;

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
        // First hit
        ApplyDamage();

        // Wait for delay
        yield return new WaitForSeconds(delayBetweenHits);

        // Second hit
        ApplyDamage();

        // Reset attack trigger and allow attacking again
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }

    public void ApplyDamage()
    {
        Debug.Log("Dealing " + damagePerHit + " damage");
        // Add your code here to deal damage to an enemy
    }
}
