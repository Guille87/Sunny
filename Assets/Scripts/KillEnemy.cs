using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] AudioClip dieFX;
    [SerializeField] GameObject dieAnim;
    [SerializeField] float bounceForce = 5f; // Fuerza del rebote

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reproducir el sonido de muerte
            AudioSource.PlayClipAtPoint(dieFX, transform.position, 0.5f);

            // Instanciar la animación de muerte
            Instantiate(dieAnim, transform.position, Quaternion.identity);

            // Destruir el enemigo
            Destroy(gameObject);

            // Aplicar fuerza de rebote al jugador
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
            }
        }
    }
}
