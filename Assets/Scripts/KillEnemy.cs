using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] AudioClip dieFX;
    [SerializeField] GameObject dieAnim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(dieFX, transform.position, 0.5f);
            Instantiate(dieAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
