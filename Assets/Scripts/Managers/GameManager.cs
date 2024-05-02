using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    enum Game
    {
        NoGame,
        Game1Player,
        Game2Players
    }

    [SerializeField] Game game;
    [SerializeField] GameObject tankPrefab;
    [SerializeField] GameObject uiObject;
    [SerializeField] GameObject Player1WinsCanvas;
    [SerializeField] GameObject Player2WinsCanvas;

    private int Player1Score;
    private int Player2Score;

    private bool isGameEnded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {

    }

    public static Vector2 GetBorder()
    {
        Vector2 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return stageDimensions;
    }

    internal void EndGame(int defeatedPlayerNumber)
    {
        if (!isGameEnded)
        {

            if (defeatedPlayerNumber == 1)
            {
                Player2Wins();
            }
            else if (defeatedPlayerNumber == 2)
            {
                Player1Wins();
            }
        }
        isGameEnded = true;
    }

    private void Player1Wins()
    {
        Player1WinsCanvas.SetActive(true);
    }

    private void Player2Wins()
    {
        Player2WinsCanvas.SetActive(true);
    }
}
