using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] Color tankColor;   // цвет танка

    [SerializeField] SpriteRenderer muzzleSpriteMask;
    [SerializeField] SpriteRenderer headerSpriteMask;
    [SerializeField] SpriteRenderer footerSpriteMask;

    public void SetColor(Color color)
    {


        tankColor = color;

        muzzleSpriteMask.color = color;
        headerSpriteMask.color = color;
        footerSpriteMask.color = color;
    }
}
