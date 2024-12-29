using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] AudioClip dieFX;
    [SerializeField] CinemachineCamera followCamera;
    Animator anim;
    Vector3 initialPosition;
    Rigidbody2D rb;
    Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        initialPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DieAndRebord());
        }
    }

    IEnumerator DieAndRebord()
    {
        // Eliminar la velocidad del jugador
        rb.linearVelocity = Vector2.zero;

        // Reproducir el sonido de muerte
        AudioSource.PlayClipAtPoint(dieFX, transform.position, 0.5f);

        // Activar la animación de muerte del jugador
        anim.SetTrigger("die");

        // Desactivar la cámara de seguimiento
        followCamera.enabled = false;
        
        // Desactivar el collider del jugador
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        col.enabled = false;

        yield return new WaitForSeconds(3);

        // Reiniciar la posición del jugador
        transform.position = initialPosition;

        // Reiniciar la animación del jugador
        anim.SetTrigger("reborn");

        // Activar el collider del jugador
        col.enabled = true;

        // Activar la cámara de seguimiento
        followCamera.enabled = true;
    }
}
