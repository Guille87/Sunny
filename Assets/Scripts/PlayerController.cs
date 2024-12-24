using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform groundCheck; // Punto de verificación del suelo
    [SerializeField] float groundCheckRadius = 0.1f; // Radio del círculo de verificación
    [SerializeField] LayerMask groundLayers; // Capa que define el suelo

    Rigidbody2D rb;
    Animator anim;

    float moveX;
    bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        anim.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon);
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
        
        anim.SetTrigger("isJumping");
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
