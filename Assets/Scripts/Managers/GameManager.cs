using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] GameTypes gameType;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] SceneAsset[] scenes2Players;

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

    public static void LoadRandomScene()
    {
        if (Instance.gameType == GameTypes.Game2Players)
        {
            int scenesCount = Instance.scenes2Players.Length;
            if (scenesCount > 0)
            {
                SceneAsset randomScene = Instance.scenes2Players[Random.Range(0, scenesCount)];
                string randomSceneString = randomScene.name;
                Instance.LoadScene(randomSceneString);
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
}
