
    using System;
    using System.Collections;
    using UnityEngine;

    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;
        public bool levelHasEnded;
        [Tooltip("Amount of bullets given for this level.")]
        public int bullets;
        [Tooltip("Amount of bullets remaining required for three stars.")]
        public int threeStars;
        [Tooltip("Amount of bullets remaining required for two stars.")]
        public int twoStars;
        [HideInInspector]
        public int stars;
        private bool _finalLevelCheckActive;
        private int _enemiesToEliminate;
        private int _enemiesEliminated;
        private bool _levelStarted;
        public UIController uiController;
        public CameraFollow cam;

        public static event Action<int> OnBulletsChanged;
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
        }

        public void Init(LevelObject currentLevel)
        {
            if (currentLevel == null)
            {
                Debug.LogError("Level Controller tried to initialize a null level");
                return;
            }
            _finalLevelCheckActive = false;
            _enemiesToEliminate = 0;
            _enemiesEliminated = 0;
            _levelStarted = false;
            threeStars = currentLevel.threeStars;
            twoStars = currentLevel.twoStars;
            bullets = currentLevel.bullets;
            levelHasEnded = false;
            stars = 3;
            uiController.Init(bullets);
            cam.Init(currentLevel.initialCameraPosition);
        }
        public void AddEnemy()
        {
            _levelStarted = true;
            _enemiesToEliminate++;
        }
        

        public void EnemyDying()
        {
            _enemiesEliminated++;
            if (_enemiesToEliminate<=_enemiesEliminated)
            {
                StartCoroutine(LevelWon());
            }
        }
        public IEnumerator LevelWon()
        {
            if (levelHasEnded)
            {
                yield break;
            }
            levelHasEnded = true;
            if (bullets >= threeStars)
            {
                stars = 3;
            }
            else if (bullets >= twoStars)
            {
                stars = 2;
            }
            else
            {
                stars = 1;
            }
            GameController.Instance.LevelCompleted(stars);
            yield return new WaitForSeconds(1);
            UIController.Instance.LevelComplete(stars, GameController.Instance.LastLevel());
        }
        public bool TryDecreaseBullets()
        {
            if (bullets<=0)
            {
                return false;
            }
            bullets--;
            OnBulletsChanged?.Invoke(bullets);
            return true;
        }

        public void UpdateStarCount()
        {
            if (levelHasEnded)
            {
                return;
            }
            if (bullets == threeStars)
            {
                stars = 2;
                UIController.Instance.SetTwoStars();
            }
            else if (bullets == twoStars)
            {
                stars = 1;
                UIController.Instance.SetOneStar();
            }
        }

        IEnumerator FinalLevelCheck()
        {
            yield return new WaitForSeconds(2);
            LevelFailed();
        }

        void LevelFailed()
        {
            if (levelHasEnded) return;
            levelHasEnded = true;
            GameController.Instance.SetActive(false);
            UIController.Instance.LevelFailed();
        }

        private void Update()
        {
            if (_finalLevelCheckActive||_levelStarted == false||PlayerController.Instance==null)
            {
                return;
            }
            if (bullets == 0 && !PlayerController.Instance.IsLive())
            {
                _finalLevelCheckActive = true;
                StartCoroutine(FinalLevelCheck());
            }
        }
        
    }
