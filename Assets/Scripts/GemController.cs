using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] AudioClip gemCollect;
    [SerializeField] Animator animCollect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Lanzamos el sonido de recogida de las gemas
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(gemCollect, transform.position);

            // Lanzamos la animación de recogida de las gemas
            Instantiate(animCollect, transform.position, Quaternion.identity);

            // Modificar volumen y pitch del sonido de recogida de las gemas
            AudioSource.PlayClipAtPoint(gemCollect, transform.position, 0.5f);

            // Destruimos la gema
            Destroy(gameObject);
        }
    }
}
