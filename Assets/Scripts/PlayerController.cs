using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    Rigidbody2D rb;
    Collider2D col;

    float moveX;
    bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        Run();
        Flip();
        Jump();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if (!jump && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void Run()
    {
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.linearVelocity.y);
        rb.linearVelocity = vel;
    }

    void Flip()
    {
        float vx = rb.linearVelocity.x;

        if (Mathf.Abs(vx) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(vx), 1);
        }
    }

    void Jump()
    {
        if (!jump) return;

        jump = false;

        if (!col.IsTouchingLayers(LayerMask.GetMask("Terrain", "Platforms")))
            return;
        
        rb.linearVelocity += new Vector2(0, jumpSpeed);
    }
}
