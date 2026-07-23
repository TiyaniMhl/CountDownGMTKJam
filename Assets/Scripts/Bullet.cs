using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float _speed;
    private Vector2 _direction;
    private const float LifeTime = 5;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.gravityScale = 0;
        rb.linearVelocity = transform.right * PlayerController.Instance.bulletSpeed;
        Destroy(gameObject, LifeTime);
    }
    

    void FixedUpdate()
    {
        
    }
    

    void Move()
    {
        
    }
}
