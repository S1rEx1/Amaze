using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float collisionBounceForce = 1.5f;
    
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Эффект "отскока" при столкновении
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bounceDirection * collisionBounceForce, ForceMode2D.Impulse);
        }
    }
}
