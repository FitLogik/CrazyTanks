using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : TankController
{
    [Header("Player Properties")]
    public int playerNumber;
    
    


    #region Axis
    string _movementAxisName;
    string _rotateAxisName;
    string _fireAxisName;
    #endregion





    protected override void Start()
    {
        base.Start();

        // Присваиваем названия осей для ввода (Horizontal1/2, Vertical1/2, Fire1/2), где 1/2 - один из номеров игрока
        _movementAxisName = "Horizontal" + playerNumber;
        _rotateAxisName = "Vertical" + playerNumber;
        _fireAxisName = "Fire" + playerNumber;

        SetColor(PrefsManager.GetPlayerColor(playerNumber));

        SetPosition(playerNumber == 1 ? Vector2.left : Vector2.right);
    }

    protected override void Update()
    {
        base.Update();

        #region Стрельба

        moveInput = Input.GetAxisRaw(_movementAxisName);
        rotationInput = Input.GetAxisRaw(_rotateAxisName); // поворот дула вверх (1), вниз (-1), или нет поворота (0)
        fireInput = Input.GetAxisRaw(_fireAxisName);
        
        #endregion
    }

    internal override void IncreaseHealth(int value)
    {
        float lastHealth = health;

        base.IncreaseHealth(value);

        Debug.Log($"Increase Health Player{playerNumber} ({lastHealth} => {health})");
    }

    internal override void TakeDamage(int value)
    {
        base.TakeDamage(value);

        if (health <= 0)
        {
            RoundManager.instance.EndRound(playerNumber);
        }

        BonusUIController.RemoveShield(playerNumber);
    }

    internal override void Freeze(float freezeDuration)
    {
        Debug.Log($"Freeze Player{playerNumber}");
        base.Freeze(freezeDuration);
    }

    protected override void Fire()
    {
        base.Fire();

        Debug.Log($"Fire\nPlayer{playerNumber}");
    }

    protected override Projectile CreateProjectile()
    {
        Projectile projectile = base.CreateProjectile();

        projectile.properties.owner = playerNumber;

        return projectile;
    }

    public override void ActivateShield()
    {
        base.ActivateShield();

        Debug.Log($"Shield Player{playerNumber}");
    }


}

