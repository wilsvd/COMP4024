using UnityEngine;

// The PlayerMovement class handles the movement and behavior of the player character.
public class PlayerMovement : MonoBehaviour
{
    internal SpriteRenderer sprite;
    internal Rigidbody2D rb;
    // Movement parameters
    internal float moveSpeed = 7f; // Speed of horizontal movement.
    internal float jumpForce = 7f; // Force applied when jumping.
    internal float fallMultiplier = 4.5f; // Custom multiplier for faster falling.
    public bool canMove = true; // Flag to control whether the player can move.
    private bool facingRight = true; // Flag to track the direction the player is facing.

    public int jumpCount = 0; // Number of jumps the player has performed.

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Get horizontal and vertical input from the player.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Call the MovePlayer method to handle player movement.
        MovePlayer(inputX, inputY, Input.GetButtonDown("Jump"), Time.deltaTime);
    }

    public void MovePlayer(float inputX, float inputY, bool isJump, float deltaTime)
    {
        if(canMove == false)
        {
            rb.velocity = new Vector2(0, 0); // Set velocity to zero if movement is not allowed.
            return;
        }
        // Apply horizontal movement to the player.
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        // Flip the player character's sprite if changing direction.
        if (inputX > 0.5 && !facingRight)
        {
            Flip();
        }
        else if (inputX < -0.5 && facingRight)
        {
            Flip();
        }

        if (isJump)
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

    // Flip method reverses the direction the player is facing.
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
            // Reset jump count when colliding with an object tagged as "Ground."
            jumpCount = 0;
        }
    }
}
