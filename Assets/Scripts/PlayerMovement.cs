using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    internal Rigidbody2D rb;
    internal float moveSpeed = 7f;
    internal float jumpForce = 7f;
    internal float fallMultiplier = 4.5f;

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
        if (inputY > 0.1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (inputY < -0.1)
        {
            // Applying a custom fall multiplier when falling
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * deltaTime;
        }
    }
}
