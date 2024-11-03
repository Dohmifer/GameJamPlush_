using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float jumpCooldown = 1f;

    private bool canJump = true;
    private Rigidbody2D rb;
    private float jumpCooldownTimer = 0f;
    private Animator animator;
    private bool isFacingRight = true; // Track the direction the player is facing

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isMoving = false;

        // Horizontal movement
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            isMoving = true;
            if (isFacingRight) Flip(); // Flip to face left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            isMoving = true;
            if (!isFacingRight) Flip(); // Flip to face right
        }

        // Set the "IsMoving" parameter in Animator
        animator.SetBool("IsMoving", isMoving);

        // Jump if space is pressed and not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            animator.SetBool("IsJumping", true);
        }

        // Handle cooldown timer for jump
        if (!canJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
            {
                canJump = true;
                animator.SetBool("IsJumping", false); // Reset jumping animation after cooldown
            }
        }

        // Set yVelocity in Animator (for tracking vertical movement)
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply upward force for jump
        canJump = false;
        jumpCooldownTimer = jumpCooldown;
    }

    // Method to flip the character's facing direction
    private void Flip()
    {
        isFacingRight = !isFacingRight; // Toggle the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the x-axis scale
        transform.localScale = scale;
    }

    // Attack example method
    public void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("IsAttacking", true);
    }

    // Methods for skills
    public void UseFirstSkill()
    {
        animator.SetBool("CanFirstSkill", true);
        animator.SetTrigger("FirstSkill");
        animator.SetBool("IsFirstSkill", true);
    }

    public void UseSecondSkill()
    {
        animator.SetBool("CanSecondSkill", true);
        animator.SetTrigger("SecondSkill");
        animator.SetBool("IsSecondSkill", true);
    }

    public void UseThirdSkill()
    {
        animator.SetBool("CanThirdSkill", true);
        animator.SetTrigger("ThirdSkill");
        animator.SetBool("IsThirdSkill", true);
    }

    // Resetting skill states (after the animation or skill duration ends)
    public void ResetSkillStates()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsFirstSkill", false);
        animator.SetBool("IsSecondSkill", false);
        animator.SetBool("IsThirdSkill", false);
    }

    public void SetIsDead(bool isDead)
    {
        animator.SetBool("IsDead", isDead);
    }

    public void SetIsDashing(bool isDashing)
    {
        animator.SetBool("IsDashing", isDashing);
    }
}
