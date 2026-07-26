using System;
using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Animator _animator;
    private static readonly int DestroyThis = Animator.StringToHash("Destroy");
    public Collider2D triggerCollider;
    protected bool AlreadyHit;
    private AudioSource _audioSource;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        AlreadyHit = false;
        _audioSource = GetComponent<AudioSource>();
    }

    
    protected virtual void Hit()
    {
        if (AlreadyHit) return;
        AlreadyHit = true;
        StartCoroutine(BeginDestruction());
    }

    protected IEnumerator BeginDestruction()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
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
            Hit();
        }
    }
}
