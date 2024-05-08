using UnityEngine;
using UnityEngine.UI;

public class TankUIController : MonoBehaviour
{
    [SerializeField] Image muzzleMask;
    [SerializeField] Image headerMask;
    [SerializeField] Image footerMask;

    public void SetColor(Color color)
    {
        muzzleMask.color = color;
        headerMask.color = color;
        footerMask.color = color;
    }
}
