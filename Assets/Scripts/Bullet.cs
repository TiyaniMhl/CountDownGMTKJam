using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float _speed;
    private Vector2 _direction;
    private const float LifeTime = 5;
    private int _enemyCount;
    //private int _bounce;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.gravityScale = 0;
        rb.linearVelocity = transform.right * PlayerController.Instance.bulletSpeed;
        _enemyCount = 2;
        //_bounce = 2;
        Destroy(gameObject, LifeTime);
    }

    public void EnemyHit()
    {
        _enemyCount--;
        if (_enemyCount<=0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        
    }
    

    void Move()
    {
        
    }
}
