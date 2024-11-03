using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float jumpCooldown = 1f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform shootPoint; // Reference to the shoot point

    private bool canJump = true;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Animator animator;
    private float jumpCooldownTimer = 0f;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isMoving = false;

        // Update ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsJumping", !isGrounded);

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

        animator.SetBool("IsMoving", isMoving);

        // Jump if space is pressed and not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
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
                animator.SetBool("IsJumping", false);
            }
        }

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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        jumpCooldownTimer = jumpCooldown;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Flip the shootPoint position
        Vector3 shootPointScale = shootPoint.localPosition;
        shootPointScale.x *= -1;
        shootPoint.localPosition = shootPointScale;
    }
}
