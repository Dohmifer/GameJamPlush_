using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class najib_jalanScript : MonoBehaviour
{
    public float spd;
    private bool grounded;
    public float JumpSpeed;
    Rigidbody2D rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float jalan = Input.GetAxis("Horizontal") * spd * Time.deltaTime;
        transform.Translate(jalan, 0, 0);

        if (grounded == true)
        {
            PlayerJump();
        }
    }

     void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("tanah"))
        {
            grounded = true;
        }
        if (other.gameObject.CompareTag("mp"))
        {
            this.transform.parent = other.transform;
            grounded = true;
        }
    } 

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("tanah"))
        {
            grounded = false;
        }
        if (col.gameObject.CompareTag ("mp"))
        {
            this.transform.parent = null;
            grounded = false;
        }
    }

     void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, JumpSpeed * 10));
            Debug.Log("lompat");
        }
    }
}
