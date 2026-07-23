using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool levelCompleted;
    public bool levelFailed;
    public Enemy[] enemies;
    [Tooltip("Amount of bullets given for this level.")]
    public int bullets;
    [Tooltip("Amount of bullets remaining required for three stars.")]
    public int threeStars;
    [Tooltip("Amount of bullets remaining required for two stars.")]
    public int twoStars;
    [HideInInspector]
    public int stars;

    public static event Action OnThreeStars;
    public static event Action OnTwoStars;
    public static event Action OnOneStar;
    public static event Action<int> OnBulletsChanged;
    public static event Action<int> OnLevelComplete;
    public static event Action OnLevelFailed;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        levelCompleted = false;
        levelFailed = false;
        stars = 3;
        OnBulletsChanged?.Invoke(bullets);
        OnThreeStars?.Invoke();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


    void Update()
    {
        if (enemies == null)
        {
            return;
        }

        if (enemies.Length==0)
        {
            levelCompleted = true;
            OnLevelComplete?.Invoke(stars);
            return;
        }

        if (bullets == 0 && !PlayerController.Instance.IsLive())
        {
            levelFailed = true;
            OnLevelFailed?.Invoke();
        }
    }
}
