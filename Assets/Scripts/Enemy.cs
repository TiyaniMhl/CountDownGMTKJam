using System;
using System.Collections;
using UnityEngine;

public class Enemy : Destructible
{
    public bool dying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameController.Instance.AddEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override IEnumerator Hit()
    {
        if (AlreadyHit) yield break;
        AlreadyHit = true;
        dying = true;
        Animator.SetTrigger(DestroyThis);
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameController.Instance.RemoveEnemy(gameObject);
    }
}
