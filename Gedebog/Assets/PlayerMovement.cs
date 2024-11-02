using UnityEngine;

public class CharacterController2D_WithJump : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Get input for horizontal movement
        movement.x = Input.GetAxisRaw("Horizontal");

        // Move character horizontally
        transform.position += new Vector3(movement.x, 0, 0) * speed * Time.deltaTime;

        // Flip character based on horizontal movement
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }

        // Jumping when pressing Space and if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Prevent double jumping
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if character is grounded by colliding with the ground
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
