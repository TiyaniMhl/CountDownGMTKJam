using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    private List<bool> _levelsCompleted;
    private List<int> _levelStars;
    public int continueFromLevel;
    private LevelObject _currentLevel;
    private bool _active = true;
    [HideInInspector] public bool playOrContinue = false;
    private bool _gamePaused;
    private bool _onMainMenu;
    public bool debugLevel;

    public int debug;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (debugLevel)
        {
            Load();
            SceneController.Instance.Play(debug);
            return;
        }
        Init(null);
    }
    
    public void Init(LevelObject level)
    {
        
        if (level == null)
        {
            Load();
            //UnityEngine.Cursor.visible = true;
            _currentLevel = LevelDatabase.MainMenu();
            _active = false;
            //MenuController.Instance.ContinueGame(_continueFromLevel!=1);
            playOrContinue = (continueFromLevel != 1);
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
        SceneController.Instance.Play(continueFromLevel);
    }
    


    public void ResetGame()
    {
        SaveSystem.Delete();
        Init(null);
    }

    public void SaveAndQuit()
    {
        
        Time.timeScale = 1f;
        Save();
        SceneController.Instance.BackToMainMenu();
        _gamePaused = false;
        Init(null);
    }

    public void Quit()
    {
        // Save();
        // Exits play mode if running inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Closes the application if running a built version
        Application.Quit();
    }
    

    void Save()
    {
        SaveData saveData = new SaveData
        {
            levelsCompleted = _levelsCompleted,
            levelStars = _levelStars
        };
        if (_currentLevel.levelNumber!=0)
        {
            saveData.continueFromLevel = _currentLevel.levelNumber;
        }
        else
        {
            saveData.continueFromLevel = 1; //Fail safe in case, this should not be possible
        }
        SaveSystem.SaveGame(saveData);
    }

    void Load()
    {
        SaveData data = SaveSystem.LoadGame();
        _levelsCompleted = data.levelsCompleted;
        _levelStars = data.levelStars;
        continueFromLevel = data.continueFromLevel;
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
        _levelsCompleted[_currentLevel.levelNumber] = true;
        _levelStars[_currentLevel.levelNumber] = s;
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
