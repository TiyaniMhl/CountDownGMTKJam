
using System;
using UnityEngine;

public class UIController:MonoBehaviour
{
    public WinUI winUI;
    public GameObject loseUI;
    public LevelUI levelUI;
    public GameObject pauseUI;

    private void OnEnable()
    {
        GameController.OnLevelComplete += LevelComplete;
        GameController.OnLevelFailed += LevelFailed;
        GameController.FinalLevel += NoNextLevel;
        GameController.OnPause += ShowPauseMenu;
        GameController.OnResume += HidePauseMenu;
        
    }

    void Awake()
    {
        loseUI.SetActive(false);
        winUI.gameObject.SetActive(false);
        levelUI.gameObject.SetActive(true);
        pauseUI.SetActive(false);
    }

    private void OnDisable()
    {
        GameController.OnLevelComplete -= LevelComplete;
        GameController.OnLevelFailed -= LevelFailed;
        GameController.FinalLevel -= NoNextLevel;
        GameController.OnPause -= ShowPauseMenu;
        GameController.OnResume -= HidePauseMenu;
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

    public void NoNextLevel()
    {
        winUI.HideNextLevelButton();
    }

    public void LevelComplete(int s)
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
    }

    public void LevelFailed()
    {
        levelUI.gameObject.SetActive(false);
        loseUI.SetActive(true);
    }
}
