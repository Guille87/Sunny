using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform groundCheck; // Punto de verificación del suelo
    [SerializeField] float groundCheckRadius = 0.1f; // Radio del círculo de verificación
    [SerializeField] LayerMask groundLayers; // Capa que define el suelo

    Rigidbody2D rb;

    float moveX;
    bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (!IsGrounded()) return;
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
    }

    bool IsGrounded()
    {
        // Verifica si el punto groundCheck está tocando la capa del suelo
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el círculo de verificación del suelo en el editor para depuración
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
