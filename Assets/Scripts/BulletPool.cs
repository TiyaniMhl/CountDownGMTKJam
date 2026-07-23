
using System;
using UnityEngine;

public class BulletPool: MonoBehaviour
{
    public static BulletPool Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Add(GameObject bulletPrefab, Vector2 gunPosition, Quaternion rot)
    {
        Instantiate(bulletPrefab, gunPosition, rot, transform);
    }

    public bool IsLive()
    {
        return transform.childCount > 0;
    }
}
