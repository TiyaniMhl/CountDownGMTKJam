using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnQuit()
    {
        GameController.Instance.Quit();
    }

    public void OnPlay()
    {
        GameController.Instance.Play();
    }
}
