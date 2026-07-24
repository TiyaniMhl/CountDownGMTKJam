using System;
using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Animator _animator;
    private static readonly int DestroyThis = Animator.StringToHash("Destroy");
    public Collider2D triggerCollider;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    
    private IEnumerator Hit()
    {
        _animator.SetTrigger(DestroyThis);
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().SomethingHit();
            StartCoroutine(Hit());
        }
    }
}
