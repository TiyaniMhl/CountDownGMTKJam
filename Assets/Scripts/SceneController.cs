
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance;
        private static readonly string MainMenu = "Scenes/MainMenu";

        
        //private bool _onMainMenu;
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        
        public void Play(int lo)
        {
            LevelObject requestedLevel = LevelDatabase.FindLevel(lo);
            PlayLevel(requestedLevel, lo);
        }

        public void PlayLevel(LevelObject requestedLevel, int lo)
        {
            if (requestedLevel==null)
            {
                Debug.LogWarning("Scene controller attempted to play a level that does not exist in the inspector. Requested level: " +lo);
                SceneManager.LoadScene(MainMenu);
                GameController.Instance.Init(null);
                //_onMainMenu = true;
                return;
            }
            if (!DoesSceneExist(requestedLevel.FullSceneName()))
            {
                Debug.LogWarning("Game controller attempted to play a scene that does not exist in the game. Requested scene: " + requestedLevel.FullSceneName());
                SceneManager.LoadScene(MainMenu);
                GameController.Instance.Init(null);
                //_onMainMenu = true;
                return;
            }
            //_onMainMenu = requestedLevel.FullSceneName() == MainMenu;
            SceneManager.LoadScene(requestedLevel.FullSceneName());
            GameController.Instance.Init(requestedLevel);
        }
        bool DoesSceneExist(string scenePath)
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
            return buildIndex >= 0;
        }

        

        public void BackToMainMenu()
        {
            //_onMainMenu = true;
            SceneManager.LoadScene(MainMenu);
        }

        
    }
