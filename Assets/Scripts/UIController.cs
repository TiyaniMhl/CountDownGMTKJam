
using System;
using UnityEngine;

public class UIController:MonoBehaviour
{
    public WinUI winUI;
    public LoseUI loseUI;
    public LevelUI levelUI;

    private void OnEnable()
    {
        GameController.OnLevelComplete += LevelComplete;
        GameController.OnLevelFailed += LevelFailed;
        GameController.FinalLevel += NoNextLevel;
        loseUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
        levelUI.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameController.OnLevelComplete -= LevelComplete;
        GameController.OnLevelFailed -= LevelFailed;
        GameController.FinalLevel -= NoNextLevel;
    }

    public void OnNextLevel()
    {
        GameController.Instance.NextLevel();
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
        loseUI.gameObject.SetActive(true);
    }
}
