using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreService scoreService;


    ScoreUIController scoreUIController;

    private void Awake()
    {
        if (scoreService == null)
        {
            scoreService = new ScoreService();
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;   // SceneManager_sceneLoaded ����� ����������� ������ ���,
                                                                // ����� ����� ����������� ����� �����
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Scene loaded");
        FindScoreUIController();
    }

    private void FindScoreUIController()
    {

        // ���� �� ����� ������, ����������� ������ � ���������������� ����������
        scoreUIController = FindObjectOfType<ScoreUIController>();
        if (scoreUIController == null)
        {
            Debug.LogError("��������� ������ ��� ������ ScoreUIController!\n" +
                           "�� ������� ����� ������ ScoreUIController � ������� �����.");
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


    public string GetScoreText()
    {
        return scoreService.GetScore(GameManager.GameType);
    }
}
