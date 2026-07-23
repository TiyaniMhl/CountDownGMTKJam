using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        if (Camera.main == null) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        transform.position = mousePos;
    }
}
