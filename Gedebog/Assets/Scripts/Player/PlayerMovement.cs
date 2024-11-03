using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGameOver = false; // Tambahkan ini

    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the Player object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Cek jika rb sudah terinisialisasi dan tidak dalam keadaan Game Over
        if (rb == null || isGameOver) return;

        float moveInput = 0;

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Raycast untuk mengecek apakah di tanah
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

