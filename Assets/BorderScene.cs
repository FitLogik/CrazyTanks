using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScene : MonoBehaviour
{
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

        Vector2 borders = GameManager.GetBorders();

        walls.points = new Vector2[]
        {
            new Vector2(-borders.x - 0.33f, -borders.y),
            new Vector2(-borders.x - 0.33f, borders.y * 5), // Умножаем на 5, чтобы стена была высоко 
            new Vector2(borders.x + 0.33f, borders.y * 5),  // и пули не уничтожались при попадании в потолок
            new Vector2(borders.x + 0.33f, -borders.y)
        };
    }
}
