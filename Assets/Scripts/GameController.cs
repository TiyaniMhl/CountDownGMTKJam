using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool levelHasEnded;
    
    private List<GameObject> _enemies;
    [Tooltip("Amount of bullets given for this level.")]
    public int bullets;
    [Tooltip("Amount of bullets remaining required for three stars.")]
    public int threeStars;
    [Tooltip("Amount of bullets remaining required for two stars.")]
    public int twoStars;
    [HideInInspector]
    public int stars;

    private bool _levelStarted;

    private List<bool> _levelsCompleted;
    private List<int> _levelStars;
    private int _continueFromLevel;
    private LevelObject _currentLevel;
    private bool _paused = false;
    private bool _finalLevelCheckActive;

    private List<LevelObject> _allLevels;
    [HideInInspector] public bool playOrContinue = false;
    
    public static event Action OnThreeStars;
    public static event Action OnPause;
    public static event Action OnResume;
    public static event Action OnTwoStars;
    public static event Action OnOneStar;
    public static event Action FinalLevel;
    public static event Action<int> OnBulletsChanged;
    public static event Action<int> OnLevelComplete;
    public static event Action OnLevelFailed;

    private static readonly string MainMenu = "Scenes/MainMenu";
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        BuildLevelList();
        Init(null);
    }

    void BuildLevelList()
    {
        _allLevels = Resources.LoadAll<LevelObject>("Level Objects").ToList();
    }

    void Init(LevelObject level)
    {
        
        if (level == null)
        {
            //UnityEngine.Cursor.visible = true;
            _currentLevel = FindLevel(0);
            Load();
            //MenuController.Instance.ContinueGame(_continueFromLevel!=1);
            playOrContinue = (_continueFromLevel != 1);
            return;
        }

        _finalLevelCheckActive = false;
        //UnityEngine.Cursor.visible = false;
        _enemies = new List<GameObject>();
        _levelStarted = false;
        _currentLevel = level;
        threeStars = _currentLevel.threeStars;
        twoStars = _currentLevel.twoStars;
        bullets = _currentLevel.bullets;
        levelHasEnded = false;
        stars = 3;
        OnBulletsChanged?.Invoke(bullets);
        OnThreeStars?.Invoke();
        int nxtLevel = level.levelNumber + 1;
        if (FindLevel(nxtLevel)==null)
        {
            FinalLevel?.Invoke();
        }
    }

    public void Play()
    {
        LevelObject requestedLevel = FindLevel(_continueFromLevel);
        PlayLevel(requestedLevel);
    }

    void PlayLevel(LevelObject requestedLevel)
    {
        if (requestedLevel==null)
        {
            Debug.LogWarning("Game controller attempted to play a level that does not exist in the inspector. Requested level: " + _continueFromLevel);
            SceneManager.LoadScene(MainMenu);
            Init(null);
            return;
        }
        if (!DoesSceneExist(requestedLevel.FullSceneName()))
        {
            Debug.LogWarning("Game controller attempted to play a scene that does not exist in the game. Requested scene: " + requestedLevel.FullSceneName());
            SceneManager.LoadScene(MainMenu);
            Init(null);
            return;
        }
        SceneManager.LoadScene(requestedLevel.FullSceneName());
        Init(requestedLevel);
    }
    bool DoesSceneExist(string scenePath)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
        return buildIndex >= 0;
    }

    public Vector3 initCameraPos()
    {
        return _currentLevel.initialCameraPosition;
    }


    public void ResetGame()
    {
        SaveSystem.Delete();
        Init(null);
    }

    public void SaveAndQuit()
    {
        Save();
        SceneManager.LoadScene(MainMenu);
        Init(null);
    }

    public void Quit()
    {
        Save();
        // Exits play mode if running inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Closes the application if running a built version
        Application.Quit();
    }

    LevelObject FindLevel(int req)
    {
        return _allLevels.FirstOrDefault(lo => lo.levelNumber == req);
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
        if (data!=null)
        {
            _levelsCompleted = data.levelsCompleted;
            _levelStars = data.levelStars;
            _continueFromLevel = data.continueFromLevel;
        }
        else
        {
            _levelsCompleted = new List<bool>();
            _levelStars = new List<int>();
            _continueFromLevel = 1;
        }
    }

    public bool GamePaused()
    {
        return _paused;
    }

    public void AddEnemy(GameObject e)
    {
        
        _enemies.Add(e);
        _levelStarted = true;
    }

    public void RemoveEnemy(GameObject e)
    {
        _enemies.Remove(e);
    }

    public void RestartLevel()
    {
        PlayLevel(_currentLevel);
    }

    public IEnumerator LevelWon()
    {
        //UnityEngine.Cursor.visible = true;
        levelHasEnded = true;
        _levelsCompleted[_currentLevel.levelNumber] = true;
        _levelStars[_currentLevel.levelNumber] = stars;
        yield return new WaitForSeconds(1);
        OnLevelComplete?.Invoke(stars);
    }

    public void NextLevel()
    {
        int nextLevel = _currentLevel.levelNumber + 1;
        LevelObject nl = FindLevel(nextLevel);
        PlayLevel(nl);
    }

    public bool TryDecreaseBullets()
    {
        if (bullets<=0)
        {
            return false;
        }
        bullets--;
        OnBulletsChanged?.Invoke(bullets);
        if (bullets<threeStars)
        {
            stars = 2;
            OnTwoStars?.Invoke();
        }
        else if (bullets<twoStars)
        {
            stars = 1;
            OnOneStar?.Invoke();
        }
        return true;
    }

    public void Pause()
    {
        if (levelHasEnded)
        {
            return;
        }
        _paused = true;
        Time.timeScale = 0f;
        //UnityEngine.Cursor.visible = true;
        OnPause?.Invoke();
    }

    public void Resume()
    {
        _paused = false;
        Time.timeScale = 1f;
        //UnityEngine.Cursor.visible = false;
        OnResume?.Invoke();
    }


    void Update()
    {
        if (_finalLevelCheckActive)
        {
            return;
        }

        if (_currentLevel.levelNumber == 0) 
        {
            return;
        }

        if (_levelStarted==false)
        {
            return;
        }

        bool allEnemiesDying = _enemies.All(enemy => enemy.GetComponent<Enemy>().dying);
        if (allEnemiesDying)
        {
            StartCoroutine(LevelWon());
            return;
        }

        if (bullets == 0 && !PlayerController.Instance.IsLive())
        {
            _finalLevelCheckActive = true;
            StartCoroutine(FinalLevelCheck());
        }
    }

    IEnumerator FinalLevelCheck()
    {
        float timer = 10f;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            bool allEnemiesDying = _enemies.All(enemy => enemy.GetComponent<Enemy>().dying);
            if (allEnemiesDying)
            {
                StartCoroutine(LevelWon());
                yield break;
            }
            yield return null; // wait one frame
        }
        LevelFailed();
    }

    void LevelFailed()
    {
        levelHasEnded = true;
        OnLevelFailed?.Invoke();
        //UnityEngine.Cursor.visible = false;
    }
}
