using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; // Set jump force
    public float jumpCooldown = 1f; // 1-second cooldown for jumping

    private bool canJump = true; // Tracks if player can jump (cooldown check)
    private Rigidbody2D rb; // Rigidbody2D for applying jump force
    private float jumpCooldownTimer = 0f; // Timer to manage jump cooldown

    private bool hadapkanan = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize Rigidbody2D
    }

    void Update()
    {
        // Horizontal movement
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            if (hadapkanan)
            {
                flip();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            if (!hadapkanan)
            {
                flip();
            }
        }

        // Jump if space is pressed and not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        // Handle cooldown timer for jump
        if (!canJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
            {
                canJump = true;
            }
        }
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
        canJump = false; // Start jump cooldown
        jumpCooldownTimer = jumpCooldown; // Reset cooldown timer to 1 second
    }

    void flip()
    {
        hadapkanan = !hadapkanan;
        Vector3 scale = transform.localScale;
             scale.x *= -1;
             transform.localScale = scale;
    }

    void OnCollisionEnter2D (Collision2D colll)
    {
        if(colll.gameObject.CompareTag("mp"))
        {
            this.transform.parent = colll.transform;
        }
    }

    void OnCollisionExit2D (Collision2D EXcol)
    {
        if (EXcol.gameObject.CompareTag("mp"))
        {
            this.transform.parent = null;
        }
    }
}
