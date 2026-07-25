using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI playText;

    public static MenuController Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuit()
    {
        GameController.Instance.Quit();
    }

    public void OnReset()
    {
        GameController.Instance.ResetGame();
    }

    public void OnPlay()
    {
        GameController.Instance.Play();
    }

    public void ContinueGame(bool c)
    {
        playText.text = c ? "Continue" : "Play";
    }
}
