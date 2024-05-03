using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public static Vector2 windDirection = Vector2.right; // —татическое поле дл€ направлени€ ветра
    public static float windStrength = 0f; // —татическое поле дл€ силы ветра

    
    [Header("Prefabs")]
    [SerializeField] GameObject Player1WinsCanvas;
    [SerializeField] GameObject Player2WinsCanvas;

    bool isRoundEnded;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }


    private void Start()
    {
        SetBorders();
    }

    private void SetBorders()
    {
        EdgeCollider2D walls = gameObject.GetComponent<EdgeCollider2D>();
        if (walls == null)
        {
            walls = gameObject.AddComponent<EdgeCollider2D>();
        }

        Vector2 borders = GetBorders();

        walls.points = new Vector2[]
        {
            new Vector2(-borders.x - 0.33f, -borders.y),
            new Vector2(-borders.x - 0.33f, borders.y * 5), // ”множаем на 5, чтобы стена была высоко 
            new Vector2(borders.x + 0.33f, borders.y * 5),  // и пули не уничтожались при попадании в потолок
            new Vector2(borders.x + 0.33f, -borders.y)
        };
    }

    public static Vector2 GetBorders()
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


    internal void EndRound(int defeatedPlayerNumber)
    {
        if (!isRoundEnded)
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
        isRoundEnded = true;
    }

    private void Player1Wins()
    {
        Player1WinsCanvas.SetActive(true);
        GameManager.instance.Player1Score++;
    }

    private void Player2Wins()
    {
        Player2WinsCanvas.SetActive(true);
        GameManager.instance.Player2Score++;
    }

}
