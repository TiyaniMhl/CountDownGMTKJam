using System;
using UnityEngine;

public class Enemy : Destructible
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameController.Instance.AddEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameController.Instance.RemoveEnemy(gameObject);
    }
}
