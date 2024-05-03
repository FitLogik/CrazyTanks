using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWins : MonoBehaviour
{
    public void LoadNextScene()
    {
        GameManager.LoadRandomScene();
    }
}
