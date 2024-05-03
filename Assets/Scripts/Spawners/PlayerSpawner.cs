using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;

    [SerializeField] GameObject tankPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static PlayerController CreatePlayer(PlayerSpawnProperties properties)
    {
        Vector2 playerPosition = GetPosition(properties.direction);

        GameObject player = Instantiate(instance.tankPrefab, playerPosition, Quaternion.identity);

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController �� ������!");
        }

        playerController.playerNumber = properties.playerNumber;

        if (properties.direction == PlayerSpawnProperties.Direction.Left)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
        }

        return playerController;
    }

    private static Vector2 GetPosition(PlayerSpawnProperties.Direction direction)
    {
        // �������� ���������� ���� ������
        Vector2 border = GameManager.GetBorder();
        // ������������� ������� �� ������ ������
        Vector2 position = new Vector2(border.x - border.x / 5, 0);

        // ������ ���� �� ����� � ���������� � ������������ � ������������
        position.x *= (int)direction;

        return position;
    }
}

public class PlayerSpawnProperties
{
    public enum Direction
    {
        Right = -1,
        Left = 1
    }

    public Color color;
    public Direction direction;
    public int playerNumber;
}
