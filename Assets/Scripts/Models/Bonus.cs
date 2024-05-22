using UnityEngine;

public enum BonusType { Health, Shield, Freeze }

public class Bonus : MonoBehaviour
{
    public BonusType bonusType;
    public int healthIncrease = 50;         // значение увеличения здоровья
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
                IncreaseHealth(playerID, healthIncrease);   // увеличение здоровья
                break;
            case BonusType.Shield:
                ActivateShield(playerID);                   // активация щита
                break;
            case BonusType.Freeze:
                ActivateFreeze(playerID, freezeDuration);   // заморозка противника
                break;
        }
    }

    private void IncreaseHealth(int playerID, int healthIncrease)
    {
        TankController tank = RoundManager.GetPlayer(playerID);

        tank.IncreaseHealth(healthIncrease);
    }

    private void ActivateShield(int playerID)
    {
        TankController tank = RoundManager.GetPlayer(playerID);

        tank.ActivateShield();

        BonusUIController.SetShield(playerID);
    }

    private void ActivateFreeze(int bonusOwnerID, float freezeDuration)
    {
        TankController enemy = FindEnemy(bonusOwnerID);

        enemy.Freeze(freezeDuration);
    }



    private TankController FindEnemy(int playerID)
    {
        int enemyID = playerID == 1 ? 2 : 1;
        return RoundManager.GetPlayer(enemyID);
    }
}
