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

    public void SetColor(Color color)
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

    public void SetIceMaterial()
    {
        SetFrameMaterial(iceMaterial);
        SetMaskMaterial(iceMaterial);
    }

    public void SetDefaultMaterial()
    {
        SetFrameMaterial(defaultMaterial);
        SetMaskMaterial(defaultMaterial);
    }
}
