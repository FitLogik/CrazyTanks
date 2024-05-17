using System.Collections;
using UnityEngine;

public class TankColorController : MonoBehaviour
{
    [Header("Mask Sprites")]
    [SerializeField] SpriteRenderer muzzleSpriteMask;
    [SerializeField] SpriteRenderer headerSpriteMask;
    [SerializeField] SpriteRenderer footerSpriteMask;

    [Header("Frame Sprites")]
    [SerializeField] SpriteRenderer muzzleSpriteFrame;
    [SerializeField] SpriteRenderer headerSpriteFrame;
    [SerializeField] SpriteRenderer footerSpriteFrame;

    [Header("Materials")]
    [SerializeField] Material iceMaterial;
    [SerializeField] Material defaultMaterial;

    public void SetMaskColor(Color color)
    {
        muzzleSpriteMask.color = color;
        headerSpriteMask.color = color;
        footerSpriteMask.color = color;
    }

    public void SetFrameMaterial(Material material)
    {
        muzzleSpriteFrame.material = material;
        headerSpriteFrame.material = material;
        footerSpriteFrame.material = material;
    }

    public void SetMaskMaterial(Material material)
    {
        muzzleSpriteMask.material = material;
        headerSpriteMask.material = material;
        footerSpriteMask.material = material;
    }

    public void SetIce()
    {
        SetMaskColor(Color.white);
        SetFrameMaterial(iceMaterial);
        SetMaskMaterial(iceMaterial);
    }

    public void SetDefaultMaterial(Color color)
    {
        SetMaskColor(color);
        SetFrameMaterial(defaultMaterial);
        SetMaskMaterial(defaultMaterial);
    }
}
