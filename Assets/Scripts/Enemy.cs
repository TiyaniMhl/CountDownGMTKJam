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
    

    protected override void Hit()
    {
        if (AlreadyHit) return;
        AlreadyHit = true;
        LevelController.Instance.EnemyDying();
        StartCoroutine(BeginDestruction());
    }
    
}
