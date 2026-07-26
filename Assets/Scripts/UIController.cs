
using UnityEngine;

public class UIController:MonoBehaviour
{
    public WinUI winUI;
    public GameObject loseUI;
    public LevelUI levelUI;
    public GameObject pauseUI;
    public static UIController Instance;

    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        loseUI.SetActive(false);
        winUI.gameObject.SetActive(false);
        levelUI.gameObject.SetActive(true);
        pauseUI.SetActive(false);
    }

    

    public void OnNextLevel()
    {
        GameController.Instance.NextLevel();
    }

    public void ShowPauseMenu()
    {
        pauseUI.gameObject.SetActive(true);
        levelUI.gameObject.SetActive(false);
    }

    public void SetTwoStars()
    {
        levelUI.TwoStars();
    }
    public void SetOneStar()
    {
        levelUI.OneStar();
    }

    public void HidePauseMenu()
    {
        pauseUI.SetActive(false);
        levelUI.gameObject.SetActive(true);
    }
    

    public void OnQuit()
    {
        GameController.Instance.SaveAndQuit();
    }

    public void OnRestartLevel()
    {
        GameController.Instance.RestartLevel();
    }

    public void Init(int bullets)
    {
        levelUI.UpdateBullets(bullets);
        levelUI.ThreeStars();
    }
    public void NoNextLevel()
    {
        winUI.HideNextLevelButton();
    }

    public void LevelComplete(int s, bool finalLevel)
    {
        levelUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(true);
        switch (s)
        {
            case 3:
                winUI.ThreeStars();
                break;
            case 2:
                winUI.TwoStars();
                break;
            case 1:
                winUI.OneStar();
                break;
        }
        if (finalLevel)
        {
            winUI.HideNextLevelButton();
        }
    }
   

    public void LevelFailed()
    {
        levelUI.gameObject.SetActive(false);
        loseUI.SetActive(true);
    }
}
