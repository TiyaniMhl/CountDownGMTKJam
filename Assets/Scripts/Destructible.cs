using System;
using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    protected Animator Animator;
    protected static readonly int DestroyThis = Animator.StringToHash("Destroy");
    public Collider2D triggerCollider;
    protected bool AlreadyHit;
    private void Start()
    {
        Animator = GetComponent<Animator>();
        AlreadyHit = false;
    }

    
    protected virtual IEnumerator Hit()
    {
        if (AlreadyHit) yield break;
        AlreadyHit = true;
        Animator.SetTrigger(DestroyThis);
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
