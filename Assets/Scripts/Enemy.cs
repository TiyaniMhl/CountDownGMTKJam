using System;
using System.Collections;
using UnityEngine;

public class Enemy : Destructible
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(WaitForLevelController());
    }
    private IEnumerator WaitForLevelController()
    {
        yield return new WaitUntil(() => LevelController.Instance != null);
        LevelController.Instance.AddEnemy();
    }
    

    protected override IEnumerator Hit()
    {
        if (AlreadyHit) yield break;
        AlreadyHit = true;
        LevelController.Instance.EnemyDying();
        Animator.SetTrigger(DestroyThis);
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
