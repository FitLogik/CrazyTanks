using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public delegate void MenuAction();
    public static event MenuAction OnMenuReturn = () => Debug.Log("OnMenuReturn Invoke");


    public static GameManager Instance { get; private set; }


    [SerializeField] GameType gameType;
    [SerializeField] ScoreManager scoreManager;

    [Header("Scene names")]
    [SerializeField] string mainMenuSceneName;
    [SerializeField] string gameLevelName;
    [SerializeField] string[] scenes2PlayersNames;
    [SerializeField] string gameOverSceneName;

    AudioManager _audioManager;

    public static GameType GameType
    {
        get { return Instance.gameType; }
        set { Instance.gameType = value; }
    }
    public static ScoreManager ScoreManager => Instance.scoreManager;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (scoreManager == null)
        {
            scoreManager = gameObject.AddComponent<ScoreManager>();
        }

        GameObject audioManagerGO = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerGO != null)
        {
            _audioManager = audioManagerGO.GetComponent<AudioManager>();
        }
        else
        {
            _audioManager = AudioManager.Instance;
            Debug.LogError("Не удалось найти объект Audio!");
        }
    }

    public static Vector2 GetBorders()
    {
        Vector2 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return stageDimensions;
    }

    public static void RoundOver()
    {
        if (GameType == GameType.Game2Players)
        {
            if (ScoreManager.IsGameOver)
            {
                Instance.LoadGameOverScene();
            }
            else
            {
                LoadRandomScene();
            }
        }
    }

    private void LoadGameOverScene()
    {
        LoadScene(gameOverSceneName);
        _audioManager.PlaySFX(_audioManager.winGame);
        AudioManager.MuteMusic(_audioManager.winGame.length);
    }

    public static void LoadRandomScene()
    {
        
        if (Instance.gameType == GameType.Game2Players)
        {
            int scenesCount = Instance.scenes2PlayersNames.Length;
            if (scenesCount > 0)
            {
                string randomScene = Instance.scenes2PlayersNames[Random.Range(0, scenesCount)];
                Instance.LoadScene(randomScene);
            }
        }
    }

    public static void LoadScene1Player(string sceneName)
    {
        if (Instance.gameType == GameType.Game1Player 
            || Instance.gameType == GameType.Game1PlayerWithBot)
        {
            Instance.gameLevelName = sceneName;   
            Instance.LoadScene(Instance.gameLevelName);
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public static void PlayerRoundWin(int playerNumber)
    {
        if (GameType == GameType.Game2Players)
        {
            ScoreManager.PlayerRoundWin(playerNumber);
        }
    }

    public static void ReturnToMainMenu()
    {
        OnMenuReturn?.Invoke();
        Instance.LoadScene(Instance.mainMenuSceneName);
        Destroy(Instance.gameObject);
    }
}
