using UnityEngine;

public enum BonusType { Health, Shield, Freeze }

public class Bonus : MonoBehaviour
{
    public BonusType bonusType;
    public int healthIncrease = 50;         // �������� ���������� ��������
    public float shieldDuration = 5f;       // ����� �������� ����
    public float freezeDuration = 5f;       // ����� �������� ���������
    public float lifeTime = 5f;             // ����� ����� ������ � ��������

    void Start()
    {
        // ��������� ������ ��� ����������� ������
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("Bonus Hit!");

            Projectile projectile = collision.collider.GetComponent<Projectile>();
            int playerID = projectile.properties.owner;

            ApplyBonus(playerID);
            Destroy(gameObject);
        }
    }

    void ApplyBonus(int playerID)
    {
        switch (bonusType)
        {
            case BonusType.Health:
                IncreaseHealth(playerID, healthIncrease);   // ���������� ��������
                break;
            case BonusType.Shield:
                ActivateShield(playerID, shieldDuration);   // ��������� ����
                break;
            case BonusType.Freeze:
                ActivateFreeze(playerID, freezeDuration);   // ��������� ����������
                break;
        }
    }

    private void IncreaseHealth(int playerID, int healthIncrease)
    {
        PlayerController player = RoundManager.GetPlayer(playerID);

        player.IncreaseHealth(healthIncrease);
    }

    private void ActivateShield(int playerID, float shieldDuration)
    {
        PlayerController player = RoundManager.GetPlayer(playerID);

        player.ActivateShield(shieldDuration);

        BonusUIController.SetShield(playerID);
    }

    private void ActivateFreeze(int bonusOwnerID, float freezeDuration)
    {
        PlayerController enemy = FindEnemy(bonusOwnerID);

        enemy.Freeze(freezeDuration);
    }



    private PlayerController FindEnemy(int playerID)
    {
        int enemyID = playerID == 1 ? 2 : 1;
        return RoundManager.GetPlayer(enemyID);
    }
}