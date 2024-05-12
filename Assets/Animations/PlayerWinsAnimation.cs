using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinsAnimation : MonoBehaviour
{
    public void LoadNextScene()
    {
        GameManager.RoundOver();
    }
}
