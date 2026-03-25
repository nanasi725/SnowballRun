using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動・ジャンプ設定")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;    
    public int maxJumps = 3;  

    [Header("成長設定")]
    public float growthRate = 0.05f;
    public float maxSize =3f;     

    private Rigidbody2D rb;         
    private int jumpCount = 0;
    private float startPosX;       
    private bool isGrounded = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosX = transform.position.x;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < maxJumps)
            {
                Jump();
            }
        }
        UpdateSize();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        
        jumpCount++;
        
        isGrounded = false;
    }

    void UpdateSize()
    {
        float distance = transform.position.x - startPosX;
        float newScale = 1f + (distance * growthRate);

        if (newScale <maxSize)
        {
            transform.localScale = new Vector3(newScale, newScale, 1f);
        }
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