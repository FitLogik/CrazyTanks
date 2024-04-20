using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum GameScenes
    {
        Game1Player = 1,
        Game2Players = 2
    }

    public void LoadGame1Player()
    {
        LoadScene(GameScenes.Game1Player);
    }

    public void LoadGame2Players()
    {
        LoadScene(GameScenes.Game2Players);
    }

    public void LoadScene(GameScenes gameScene)
    {
        SceneManager.LoadSceneAsync((int)gameScene);
    }
}
