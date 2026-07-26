
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
        //private LevelObject _currentLevel;
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
            //UnityEngine.Cursor.visible = true;
            levelHasEnded = true;
            GameController.Instance.LevelCompleted(stars);
            yield return new WaitForSeconds(1);
            UIController.Instance.LevelComplete(stars);
        }
        public bool TryDecreaseBullets()
        {
            if (bullets<=0)
            {
                return false;
            }
            bullets--;
            OnBulletsChanged?.Invoke(bullets);
            if (bullets == threeStars-1)
            {
                stars = 2;
                UIController.Instance.SetTwoStars();
            }
            else if (bullets == twoStars-1)
            {
                stars = 1;
                UIController.Instance.SetOneStar();
            }
            return true;
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
