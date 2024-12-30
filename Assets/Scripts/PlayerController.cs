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
    bool isGrounded;

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
        UpdateGroundStatus();
        UpdateJumpAndFallAnimation();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // Detectar si el jugador presiona el botón de salto
        if (!jump && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void Run()
    {
        // Movimiento horizontal
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.linearVelocity.y);
        rb.linearVelocity = vel;

        // Animación de correr o estar quieto
        anim.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon);
    }

    void Flip()
    {
        // Girar el jugador al caminar de izquierda a derecha
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

        if (!isGrounded) return;
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
    }

    void UpdateGroundStatus()
    {
        // Verifica si el jugador está tocando el suelo
        isGrounded = IsGrounded();
    }

    void UpdateJumpAndFallAnimation()
    {
        if (isGrounded)
        {
            // Si está en el suelo, desactivar salto y caída
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                // El jugador está saltando
                anim.SetBool("isJumping", true);
                anim.SetBool("isFalling", false);
            }
            else if (rb.linearVelocity.y < 0)
            {
                // El jugador está cayendo
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
            }
        }
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
