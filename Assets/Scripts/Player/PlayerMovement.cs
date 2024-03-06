using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    internal SpriteRenderer sprite;
    internal Rigidbody2D rb;
    internal float moveSpeed = 7f;
    internal float jumpForce = 7f;
    internal float fallMultiplier = 4.5f;

    private bool facingRight = true;

    public int jumpCount = 0;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        MovePlayer(inputX, inputY, Time.deltaTime);
    }

    public void MovePlayer(float inputX, float inputY, float deltaTime)
    {
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        if (inputX > 0.5 && !facingRight)
        {
            Flip();
        }
        else if (inputX < -0.5 && facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;
            }
        }

        if (inputY < -0.1)
        {
            // Applying a custom fall multiplier when falling
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1)* deltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
