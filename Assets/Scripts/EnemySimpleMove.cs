using UnityEngine;

public class EnemySimpleMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float maxDistance;
    [SerializeField] bool moveRight;

    float initialPosition;
    float direction;
    float distanceTravelled;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Posición de partida
        initialPosition = transform.position.x;

        // Sentido de movimiento
        direction = moveRight ? 1 : -1;
    }

    void Update()
    {
        float movement = speed * Time.deltaTime * direction;
        transform.Translate(new Vector2(movement, 0));

        distanceTravelled += Mathf.Abs(movement);

        if (distanceTravelled >= maxDistance)
        {
            direction *= -1;
            distanceTravelled = 0;
            sr.flipX = !sr.flipX;
        }
    }
}
