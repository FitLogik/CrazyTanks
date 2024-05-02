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

    public static Vector2 windDirection = Vector2.right; // Статическое поле для направления ветра
    public static float windStrength = 0f; // Статическое поле для силы ветра

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 windForce = windDirection * windStrength;
            rb.AddForce(windForce);
        }
    }

    public static void SetWindDirection(Vector2 newWindDirection)
    {
        windDirection = newWindDirection;
    }

    public static void SetWindStrength(float newWindStrength)
    {
        windStrength = newWindStrength;
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
