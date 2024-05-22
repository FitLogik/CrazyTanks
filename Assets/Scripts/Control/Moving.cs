using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float moveX;
    private float moveY;

    private static Moving movingObject1Player;
    private static Moving movingObject2Player;

    public static Moving GetMovingObject1Player
    {
        get
        {
            if (movingObject1Player == null)
            {
                movingObject1Player = new Moving();
            }
            return movingObject1Player;
        }
    }

    public static Moving GetMovingObject2Player
    {
        get
        {
            if (movingObject2Player == null)
            {
                movingObject2Player = new Moving();

            }
            return movingObject2Player;
        }
    }
}
