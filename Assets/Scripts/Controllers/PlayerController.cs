using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : TankController
{
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
    }

    protected override void Update()
    {
        #region Стрельба

        moveInput = Input.GetAxisRaw(_movementAxisName);
        rotationInput = Input.GetAxisRaw(_rotateAxisName); // поворот дула вверх (1), вниз (-1), или нет поворота (0)
        fireInput = Input.GetAxisRaw(_fireAxisName);
        
        #endregion

        base.Update();

    }
}

