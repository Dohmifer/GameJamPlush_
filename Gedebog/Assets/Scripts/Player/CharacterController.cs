using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CharacterController : MonoBehaviour
{
    #region // Get Komponen
    private Rigidbody2D rb;
    private Animator anim;
    #endregion
    #region // Flip Sprites dan run player
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float speedPlayer;
    #endregion
    #region // attack player dan hit enemy
    [Header("Player Attack")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange = 0.5f;
    public int playerAttack = 40;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    #endregion
    #region // deklarasi untuk jump dan double jump
    [Header("Jump & Double Jump")]
    private bool isGrounded;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int maxJumps = 1;
    #endregion

#region // Dashing
    private bool canDash = true;
    private bool isDashing;
    private Vector2 dashingDirection;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
#endregion
    
    #region // Skill
    [Header("Skill 1")]
    public GameObject fireBall;
    public Transform spawnSkill1Point;
    
    [Header("Skill 2")]
    public GameObject fireDragon;
    public Transform spawnSkill2Point;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(isDashing)
        {
            return;
        }

        MovePlayer();
        
        if (Time.time >= nextAttackTime)
        {
            //menjalankan animation attack
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        CheckGrounded();
        Jump();


        if (Input.GetKeyDown(KeyCode.E) && canDash)
         {
            StartCoroutine (Dash());
        }
        
        Skill1();
        Ultimate();
    }

    void MovePlayer()
    {
        float mHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(Input.GetAxis("Horizontal"));

        Vector2 move = new Vector2(mHorizontal , rb.velocity.y);

        rb.velocity = move * speedPlayer;

        anim.SetFloat("speed", MathF.Abs(mHorizontal * speedPlayer));
    
        //Memanggil fungsi flip
        if(mHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (mHorizontal < 0 && facingRight)
        {
            Flip();
        }
   }

   void Flip()
   {
        //mengembalikan nilai facingRight 
        facingRight = !facingRight;
        Debug.Log(facingRight);

        //mengalikan scala x dari transform untuk membalikan sprite
        Vector2 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
   }

   void Attack()
   {
        //anim.SetTrigger("attack");
        //mendeteksi attack terhadap musuh
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //memberikan damage kepada musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit Enemy" + enemy.name);
           // enemy.GetComponent<EnemyController>().TakeDamage(playerAttack);
        }

   }

   void OnDrawGizmos()
   {
        if (attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position,attackRange);

        if (isGrounded != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
   }

   void Jump()
   {
        if(isGrounded)
        {
            // reset jump
            jumpCount = 0;

            anim.SetBool("isJumping", false);
            anim.SetBool("isDoubleJump", false);

            if(Input.GetButtonDown("Jump"))
            {
                PerformJump();
            }

        }
        else 
        {
            if(Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                PerformJump();

                if(jumpCount == 2)
                {
                    anim.SetBool("isDoubleJump", true);
                }
            }
        }  
   }

   void PerformJump()
   {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJumping", true);
        jumpCount++;
   }

   void CheckGrounded()
   {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);

        if(isGrounded)
        {
            jumpCount = 1;
        }

   }

   private IEnumerator Dash()
   {
        canDash = false;
        isDashing = true;
        anim.SetBool("isDashing", true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
   }

   void Skill1()
   {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("Skill1");
        }
   }

    void Ultimate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("Skill2");
        }
    }
   void SummonSkill1()
   {
        float fireSpeed = 5f;
        //spawn transform referensi ke skill point
        GameObject fire = Instantiate(fireBall, spawnSkill1Point.position, Quaternion.identity); //instantiate summon
        Rigidbody2D rBFire = fire.GetComponent<Rigidbody2D>();

        if(facingRight)
        {
            rBFire.velocity = transform.right * fireSpeed;
        }
        else
        {
            //membalikkan agar projectile dapat ke arah kiri
            rBFire.velocity = -transform.right * fireSpeed;

            //membalikan sprite fireball
            Vector3 fireScale = fire.transform.localScale;
            fireScale.x *= -1;
            fire.transform.localScale = fireScale;
        }
   }

    void SummonSkill2()
{
    // Spawn the fire dragon at the specified spawn point for skill 2
    GameObject dragon = Instantiate(fireDragon, spawnSkill2Point.position, Quaternion.identity);
    Rigidbody2D rBDragon = dragon.GetComponent<Rigidbody2D>();

    // Set the velocity based on the player's facing direction
    if (facingRight)
    {
        rBDragon.velocity = transform.right;
    }
    else
    {
        // Flip the direction of the projectile if facing left
        rBDragon.velocity = -transform.right;

        // Flip the sprite
        Vector3 dragonScale = dragon.transform.localScale;
        dragonScale.x *= -1;
        dragon.transform.localScale = dragonScale;
    }

    // Destroy the instantiated fireDragon object after 1 second
    Destroy(dragon, 1f);
}

}
