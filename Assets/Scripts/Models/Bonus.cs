using System;
using UnityEngine;

public enum BonusType { Health, Shield, Freeze }

public class Bonus : MonoBehaviour
{
    public BonusType bonusType;
    public int healthIncrease = 50;         // значение увеличения здоровья
    public float shieldDuration = 5f;       // время действия щита
    public float freezeDuration = 5f;       // время действия заморозки
    public float lifeTime = 5f;             // время жизни бонуса в секундах

    void Start()
    {
        // Запускаем таймер для уничтожения бонуса
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("Bonus Hit!");
            int playerID = collision.collider.GetComponent<Projectile>().properties.owner;
            ApplyBonus(playerID);
            Destroy(gameObject);
        }
    }

    void ApplyBonus(int playerID)
    {
        PlayerController player = RoundManager.GetPlayer(playerID);
        switch (bonusType)
        {
            case BonusType.Health:
                player.IncreaseHealth(healthIncrease);          // увеличение здоровья
                break;
            case BonusType.Shield:
                player.ActivateShield(shieldDuration);          // активация щита
                break;
            case BonusType.Freeze:
                PlayerController enemy = FindEnemy(playerID);
                enemy.Freeze(freezeDuration);                   // заморозка противника
                break;
        }

        BonusUIController.SetBonus(playerID, bonusType, lifeTime);
    }


    private PlayerController FindEnemy(int playerID)
    {
        int enemyID = playerID == 1 ? 2 : 1;
        return RoundManager.GetPlayer(enemyID);
    }
}
