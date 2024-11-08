using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public enum EnemyType
    {
        OrcWizard,
        OrcBoomerang
    }

    public enum AreaType
    {
        Circle,
        Square
    }

    public AudioSource enemyAudio;

    [Header("Enemy")]
    public EnemyType enemyType = EnemyType.OrcWizard;
    public bool facingRight = true;
    public bool canFlip = true;
    public float moveSpeed;
    private Vector3 theScale;

    [Header("Enemy and Attack Area")]
    public AreaType areaType = AreaType.Circle;
    public LayerMask playerLayer;
    public bool inEnemyRange;
    public bool inAttackRange;

    [Header("Circle Area")]
    public Transform enemyCircleAreaCheck;
    public float enemyCircleAreaRange;
    public Transform enemyCircleAttackCheck;
    public float enemyCircleAttackRange;

    [Header("Square Area")]
    public Transform enemySquareAreaCheck;
    public Vector2 enemySquareAreaRange;
    public Transform enemySquareAttackCheck;
    public Vector2 enemySquareAttackRange;

    [Header("Ground and Wall Check")]
    public LayerMask groundLayer;
    public Transform wallCheck;
    public float wallRange;
    public bool wallDetected;
    public Transform groundCheck;
    public float groundRange;
    public bool groundDetected;

    [Header("Enemy Patrolling")]
    public float patrolSpeed;

    [Header("Enemy Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;
    public float startWaitTime;
    public bool canAttack;
    public float waitTime;

    private Rigidbody2D rb2d;
    private Animator anim;
    private Transform player;
    private Transform canvasEnemy;
    //private PlayerController playerController;
    private EnemyAttackController attackController;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        canvasEnemy = gameObject.transform.Find("CanvasEnemy");
        //playerController = FindObjectOfType<PlayerController>();
        attackController = GetComponent<EnemyAttackController>();
        attackController.SetDirection(facingRight);

        waitTime = startWaitTime;
        theScale.x = 1;
    }

    void Update()
    {
        HealthFlip();
    }

    void FixedUpdate()
    {
        GroundCheck();
        EnemyCheck();
        TargetCheck();
    }

    public void GroundCheck()
    {
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallRange, groundLayer);
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRange, groundLayer);
    }

    public void EnemyCheck()
    {
        inEnemyRange = false;
        inAttackRange = false;

        switch (areaType)
        {
            case AreaType.Circle:
                Collider2D[] enemyCircleArea = Physics2D.OverlapCircleAll(enemyCircleAreaCheck.position, enemyCircleAreaRange, playerLayer);
                Collider2D[] enemyCircleAttackArea = Physics2D.OverlapCircleAll(enemyCircleAttackCheck.position, enemyCircleAttackRange, playerLayer);

                for (int i = 0; i < enemyCircleArea.Length; i++)
                {
                    if (enemyCircleArea[i].CompareTag("Player"))
                    {
                        inEnemyRange = true;
                        break;
                    }
                }

                for (int i = 0; i < enemyCircleAttackArea.Length; i++)
                {
                    if (enemyCircleAttackArea[i].CompareTag("Player"))
                    {
                        inAttackRange = true;
                        break;
                    }
                }
                break;
            case AreaType.Square:
                Collider2D[] enemySquareArea = Physics2D.OverlapBoxAll(enemySquareAreaCheck.position, enemySquareAreaRange, 0f, playerLayer);
                Collider2D[] enemySquareAttackArea = Physics2D.OverlapBoxAll(enemyCircleAttackCheck.position, enemySquareAttackRange, 0f, playerLayer);

                for (int i = 0; i < enemySquareArea.Length; i++)
                {
                    if (enemySquareArea[i].CompareTag("Player"))
                    {
                        inEnemyRange = true;
                        break;
                    }
                }

                for (int i = 0; i < enemySquareAttackArea.Length; i++)
                {
                    if (enemySquareAttackArea[i].CompareTag("Player"))
                    {
                        inAttackRange = true;
                        break;
                    }
                }
                break;
        }
    }

    public void TargetCheck()
    {
        AttackEnd();

        if (!inEnemyRange && !inAttackRange)
        {
            waitTime = startWaitTime;
            anim.SetBool("IsMoving", true);

            if (!groundDetected && rb2d.velocity.y != 0)
            {
                canFlip = false;
            }
            else if (!groundDetected || wallDetected && rb2d.velocity.y == 0)
            {
                canFlip = true;
                Flip();
            }
            else
            {
                Vector2 targetPosition = new Vector2(transform.position.x + transform.right.x * theScale.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

                // transform.Translate((Vector2.right * theScale.x) * patrolSpeed * Time.deltaTime);
            }
        }
        else if (inEnemyRange && !inAttackRange)
        {
            waitTime = startWaitTime;
            anim.SetBool("IsMoving", true);

            if (!groundDetected || wallDetected)
            {
                anim.SetBool("IsMoving", false);
            }
            else
            {
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                canFlip = true;
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingRight)
            {
                canFlip = true;
                Flip();
            }
        }
        else if (inEnemyRange && inAttackRange)
        {
            anim.SetBool("IsMoving", false);

            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                canFlip = true;
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingRight)
            {
                canFlip = true;
                Flip();
            }

            if (!canAttack)
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0)
                {
                    canAttack = true;
                }
            }
            else
            {
                AttackCheck();
            }
        }
    }

    public void AttackCheck()
    {
        if (canAttack)
        {
            anim.SetBool("Attack", true);
        }
    }

    public void Attack()
    {
        enemyAudio.Play();

        switch (enemyType)
        {
            case EnemyType.OrcWizard:
                if (!facingRight)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 180f, 0f));
                    Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
                    rb2d.velocity = transform.right * -bulletForce;
                }
                else
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 0f));
                    Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
                    rb2d.velocity = transform.right * bulletForce;
                }
                break;
            case EnemyType.OrcBoomerang:
                if (!facingRight)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 180f, 0f));
                }
                else
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 0f));
                }
                break;
        }
    }

    public void AttackEnd()
    {
        anim.SetBool("Attack", false);
    }

    public void AttackTimeEnd()
    {
        canAttack = false;
        waitTime = startWaitTime;
    }

    public void Flip()
    {
        if (canFlip)
        {
            facingRight = !facingRight;
            attackController.SetDirection(facingRight);

            theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void HealthFlip()
    {
        if (!facingRight)
        {
            canvasEnemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            canvasEnemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallRange, wallCheck.position.y));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundRange));

        switch (areaType)
        {
            case AreaType.Circle:
                Gizmos.DrawWireSphere(enemyCircleAreaCheck.position, enemyCircleAreaRange);
                Gizmos.DrawWireSphere(enemyCircleAttackCheck.position, enemyCircleAttackRange);
                break;
            case AreaType.Square:
                Gizmos.DrawWireCube(enemySquareAreaCheck.position, enemySquareAreaRange);
                Gizmos.DrawWireCube(enemySquareAttackCheck.position, enemySquareAttackRange);
                break;
        }
    }
}
