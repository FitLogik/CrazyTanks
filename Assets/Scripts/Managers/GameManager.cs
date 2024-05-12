using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] GameTypes gameType;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] string[] scenes2PlayersNames;

    [SerializeField] string mainMenuSceneName;
    [SerializeField] string gameOverSceneName;

    public static GameTypes GameType => Instance.gameType;
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
        }

        if (GameType == GameTypes.None)
        {
            gameType = GameTypes.Game2Players;
        }

        if (scoreManager == null)
        {
            scoreManager = gameObject.AddComponent<ScoreManager>();
        }

    }

    public static Vector2 GetBorders()
    {
        Vector2 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return stageDimensions;
    }

    public static void RoundOver()
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

    private void LoadGameOverScene()
    {
        LoadScene(gameOverSceneName);
    }

    public static void LoadRandomScene()
    {
        if (Instance.gameType == GameTypes.Game2Players)
        {
            int scenesCount = Instance.scenes2PlayersNames.Length;
            if (scenesCount > 0)
            {
                string randomScene = Instance.scenes2PlayersNames[Random.Range(0, scenesCount)];
                Instance.LoadScene(randomScene);
            }
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public static void PlayerRoundWin(int playerNumber)
    {
        ScoreManager.PlayerRoundWin(playerNumber);
    }

    public static void ReturnToMainMenu()
    {
        Instance.LoadScene(Instance.mainMenuSceneName);
        Destroy(Instance.gameObject);
    }
}
