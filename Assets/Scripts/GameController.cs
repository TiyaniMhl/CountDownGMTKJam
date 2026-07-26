using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private LevelObject _currentLevel;
    private bool _active = true;
    private bool _gamePaused;
    private bool _onMainMenu;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LevelDatabase.BuildLevelList();
        Init(null);
    }
    
    public void Init(LevelObject level)
    {
        
        if (level == null)
        {
            _currentLevel = LevelDatabase.MainMenu();
            return;
        }
        _active = true;
        _currentLevel = level;
        StartCoroutine(WaitForLevelController(level));
    }
    private IEnumerator WaitForLevelController(LevelObject level)
    {
        yield return new WaitUntil(() => LevelController.Instance != null);
        LevelController.Instance.Init(level);
    }

    public void Play()
    {
        SceneController.Instance.Play(1);
    }
    
    public void SaveAndQuit()
    {
        SceneController.Instance.BackToMainMenu();
        Init(null);
    }

    public void Quit()
    {
        // Exits play mode if running inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Closes the application if running a built version
        Application.Quit();
    }

    public void SetActive(bool b)
    {
        _active = b;
    }



    public bool GameActive()
    {
        return _active;
    }
    
    public void RestartLevel()
    {
        SceneController.Instance.Play(_currentLevel.levelNumber);
    }

    public void LevelCompleted(int s)
    {
        _active = false;
    }

    public bool LastLevel()
    {
        return _currentLevel.levelNumber == 5;
    }

    public void NextLevel()
    {
        int nextLevel = _currentLevel.levelNumber + 1;
        SceneController.Instance.Play(nextLevel);
    }

    public void Pause()
    {
        if (!_active)
        {
            return;
        }
        UIController.Instance.ShowPauseMenu();
        _active = false;
        _gamePaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        UIController.Instance.HidePauseMenu();
        _active = true;
        _gamePaused = false;
        Time.timeScale = 1f;
    }

    public bool GamePaused()
    {
        return _gamePaused;
    }
    
}
