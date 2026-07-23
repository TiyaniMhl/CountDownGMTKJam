
using System;
using UnityEngine;

public class UIController:MonoBehaviour
{
    public WinUI winUI;
    public LoseUI loseUI;

    private void OnEnable()
    {
        GameController.OnLevelComplete += LevelComplete;
        GameController.OnLevelFailed += LevelFailed;
    }

    private void OnDisable()
    {
        GameController.OnLevelComplete -= LevelComplete;
        GameController.OnLevelFailed += LevelFailed;
    }

    public void LevelComplete(int s)
    {
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
        loseUI.gameObject.SetActive(true);
    }
}
