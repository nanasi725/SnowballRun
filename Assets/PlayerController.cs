using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 10f;    
    public int maxJumps = 3;        
    private Rigidbody2D rb;         
    private int jumpCount = 0;       
    private bool isGrounded = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < maxJumps)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        
        jumpCount++;
        
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f)
            {
                jumpCount = 0;     
                isGrounded = true; 
            }
        }
    }
}