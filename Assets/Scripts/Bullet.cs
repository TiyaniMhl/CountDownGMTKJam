using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float _speed;
    private Vector2 _direction;
    private const float LifeTime = 5;
    private int _pierceCount;
    private int _bounce;
    private Vector2 _trueVelocity;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.gravityScale = 0;
        rb.linearVelocity = transform.right * PlayerController.Instance.bulletSpeed;
        _pierceCount = 2;
        _bounce = 2;
        _trueVelocity = rb.linearVelocity;
        Destroy(gameObject, LifeTime);
    }

    public void SomethingHit()
    {
        _pierceCount--;
        if (_pierceCount<=0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.GetContact(0).normal;
        Vector2 current = _trueVelocity;
        if (other.gameObject.CompareTag("Surface") && !SimilarDirections(current, normal))
        {
            _trueVelocity = current - 2 * (Vector2.Dot(current, normal)) * normal;
            rb.linearVelocity = _trueVelocity;
            _bounce--;
            if (_bounce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool SimilarDirections(Vector2 a, Vector2 b)
    {
        float val = CosineSimilarity(a, b);
        return val > 0;
    }

    private float CosineSimilarity(Vector2 a, Vector2 b)
    {
        float aAbs = a.magnitude;
        float bAbs = b.magnitude;
        float dot = Vector2.Dot(a, b);
        return dot/aAbs*bAbs;
    }

    void Move()
    {
        
    }
}
