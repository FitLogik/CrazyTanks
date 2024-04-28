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

    private bool isGameEnded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (tankPrefab == null)
        {
            Debug.LogError("Префаб танка не установлен!");
            return;
        }
        else if (uiObject == null)
        {
            Debug.LogError("Объект пользовательского интерфейса не установлен!");
            return;
        }
    }

    private void Start()
    {
        // Может быть не будет использоваться данный код
        if (game == Game.Game2Players)
        {
            PlayerController player1 = CreatePlayer(1);
            PlayerController player2 = CreatePlayer(2);
        }
    }

    private PlayerController CreatePlayer(int playerNumber)
    {
        Vector2 border = GetBorder();
        Vector2 playerTransform = new Vector2(border.x - border.x / 5, 0);
        if (playerNumber == 1)
        {
            playerTransform.x *= -1;
        }

        GameObject player = Instantiate(tankPrefab, playerTransform, Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController не найден!");
        }

        playerController.playerNumber = playerNumber;
        if (playerNumber == 2)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
        }

        return playerController;
    }

    private Vector2 GetBorder()
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
