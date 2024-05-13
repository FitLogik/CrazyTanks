using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] ScoreService scoreService;


    ScoreUIController scoreUIController;


    public static bool IsGameOver => Instance.scoreService.IsGameEnded;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (scoreService == null)
        {
            scoreService = new ScoreService();
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;   // SceneManager_sceneLoaded будет выполняться каждый раз,
                                                                // когда будет загружаться новая сцена
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Scene loaded");
        if (GameManager.GameType == GameTypes.Game2Players)
        {
            FindScoreUIController();
        }
    }

    private void FindScoreUIController()
    {
        // Ищем на сцене объект, управляющий счётом в пользовательском интерфейсе
        scoreUIController = FindObjectOfType<ScoreUIController>();
        if (scoreUIController == null)
        {
            Debug.LogError("Произошла ошибка при поиске ScoreUIController!\n" +
                           "Не удалось найти объект ScoreUIController в текущей сцене.");
            return;
        }

        scoreUIController.UpdateUI(GetScoreText());
    }


    public void PlayerRoundWin(int playerNumber)
    {
        if (playerNumber == 1)
        {
            scoreService.Player1Score++;
        }
        else if (playerNumber == 2)
        {
            scoreService.Player2Score++;
        }

        scoreUIController.UpdateText(GetScoreText());
    }


    public static string GetScoreText()
    {
        return Instance.scoreService.GetScore(GameManager.GameType);
    }


    public static int? GetWinningPlayer()
    {
        return Instance.scoreService.WinningPlayer();
    }
}
