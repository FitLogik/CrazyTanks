using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    // Обозначения для 1 игрока
    private static KeyCode right1p = KeyCode.D;
    private static KeyCode left1p = KeyCode.A;

    private static KeyCode uppper1p = KeyCode.W;
    private static KeyCode down1p = KeyCode.S;

    private static KeyCode shot1p = KeyCode.Space;

    public static KeyCode Right1p { get => right1p; set => right1p = value; }
    public static KeyCode Left1p { get => left1p; set => left1p= value; }

    public static KeyCode Upper1p { get => uppper1p; set => uppper1p = value; }
    public static KeyCode Down1p { get => down1p; set => down1p= value; }
    public static KeyCode Shot1p { get => shot1p; set => shot1p = value; }


    // Обозначения для 2 игрока
    private static KeyCode right2p = KeyCode.RightArrow;
    private static KeyCode left2p = KeyCode.LeftArrow;

    private static KeyCode uppper2p = KeyCode.UpArrow;
    private static KeyCode down2p = KeyCode.DownArrow;

    private static KeyCode shot2p = KeyCode.Return;

    public static KeyCode Right2p { get => right2p; set => right2p = value; }
    public static KeyCode Left2p { get => left2p; set => left2p = value; }
    public static KeyCode Upper2p { get => uppper2p; set => uppper2p = value; }
    public static KeyCode Down2p { get => down2p; set => down2p = value; }
    public static KeyCode Shot2p { get => shot2p; set => shot2p = value; }

}
