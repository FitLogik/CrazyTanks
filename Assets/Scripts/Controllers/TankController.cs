using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TankController : TankBehaviour
{
    [Header("Tank Properties")]
    public int playerNumber;
    [SerializeField] Color tankColor;                       // цвет танка

    [Header("Misc")]
    [SerializeField] GameObject bulletSpeedCanvas;          // Холст, который имеет элемент, показывающий ускорение снаряда перед выстрелом
    [SerializeField] Image bulletSpeedBarImage;             // Прогрессбар ускорения снаряда перед выстрелом
    [SerializeField] Gradient bulletSpeedBarGradient;       // Градиент, цвета которого устанавливаются от 0 до 100
    [SerializeField] Image healthBarImage;                  // Прогрессбар здоровья игрока
    [SerializeField] Image BonusUIImage;                    // Картинка бонуса на стороне игрока


    TankColorController _colorController;
    AudioManager _audioManager;


    protected override void Awake()
    {
        base.Awake();

        GameObject audioManagerGO = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerGO != null)
        {
            _audioManager = audioManagerGO.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("Не удалось найти объект Audio!");
        }

        _colorController = GetComponent<TankColorController>();
    }

    protected override void Start()
    {
        base.Start();

        SetColor(PrefsManager.GetPlayerColor(playerNumber));
    }


    protected void SetColor(Color color)
    {
        tankColor = color;

        _colorController.SetMaskColor(tankColor);
    }



    internal override void IncreaseHealth(int value)
    {
        float lastHealth = health;

        base.IncreaseHealth(value);

        healthBarImage.fillAmount = health / maxHealth;
        if (_audioManager != null)
        {
            _audioManager.PlaySFX(_audioManager.bonusPickUp);
        }


        Debug.Log($"Increase Health Player{playerNumber} ({lastHealth} => {health})");
    }

    // Для более плавного изменения значений применяем Update вместо FixedUpdate
    protected override void Update()
    {
        base.Update();

        if (IsFireReady)
        {
            // Включаем объект с холстом прогрессбара
            bulletSpeedCanvas.SetActive(true);

            UpdateBulletSpeedBar(_bulletSpeed); // обновляем progressbar со скоростью пули

        }

        // Чтобы находящийся внутри объекта progressbar не переворачивался вместе с ним
        bulletSpeedCanvas.transform.rotation = Quaternion.identity;
    }
    
    #region UI
    private void UpdateBulletSpeedBar(float speed)
    {
        float speedAmount = (speed - minBulletSpeed) / (maxBulletSpeed - minBulletSpeed);
        bulletSpeedBarImage.fillAmount = speedAmount;
        bulletSpeedBarImage.color = bulletSpeedBarGradient.Evaluate(speedAmount);
    }
    #endregion
    #region Движение танка

    protected virtual void SetPosition(Vector2 direction)
    {
        Vector2 border = GameManager.GetBorders();
        Vector2 playerTransform = new Vector2(border.x - border.x / 5, -0.75f);

        playerTransform.x *= direction.x;

        transform.position = playerTransform;
    }

    #endregion
    #region Боевка
    protected override void Fire()
    {
        Debug.Log($"Fire\nPlayer{playerNumber}");

        base.Fire();

        // Выключаем объект с холстом прогрессбара
        bulletSpeedCanvas.SetActive(false);

        if (_audioManager != null)
        {
            _audioManager.PlaySFX(_audioManager.shot1);
        }
    }

    internal override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        healthBarImage.fillAmount = health / maxHealth; // float написан чтобы вернуло значение float, а не int

        if (health <= 0)
        {
            _colorController.SetDefaultMaterial(tankColor);
            bulletSpeedCanvas.SetActive(false);
            if (_audioManager != null)
            {
                _audioManager.PlaySFX(_audioManager.death);
            }

            RoundManager.instance.EndRound(playerNumber);
        }

        BonusUIController.RemoveShield(playerNumber);
    }

    internal override void Freeze(float freezeDuration)
    {
        Debug.Log($"Freeze Player{playerNumber}");

        bulletSpeedCanvas.SetActive(false);

        if (_audioManager != null)
        {
            _audioManager.PlaySFX(_audioManager.bonusPickUp);
        }

        base.Freeze(freezeDuration);

        StartCoroutine(SetMaterialCoroutine(freezeDuration));

    }

    IEnumerator SetMaterialCoroutine(float freezeDuration)
    {
        _colorController.SetIce();

        yield return new WaitForSeconds(freezeDuration);

        if (health > 0)
        {
            _colorController.SetDefaultMaterial(tankColor);
        }
    }

    public override void ActivateShield()
    {
        Debug.Log($"Shield Player{playerNumber}");

        if (health > 0)
        {
            base.ActivateShield();
            if (_audioManager != null)
            {
                _audioManager.PlaySFX(_audioManager.bonusPickUp);
            }
        }
    }

    #endregion
}