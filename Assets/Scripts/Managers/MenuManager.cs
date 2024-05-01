using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: давай по новой, Миша, всё...
    public enum GameScenes
    {
        Game1Player = 1,
        Game2Players = 2,
        GeneralScene = 3
    }

    public void LoadGame1Player()
    {
        LoadScene(GameScenes.Game1Player);
    }

    public void LoadGame2Players()
    {
        LoadScene(GameScenes.Game2Players);
    }

    public void LoadGeneralScene()
    {
        LoadScene(GameScenes.GeneralScene);
    }


    public void LoadScene(GameScenes gameScene)
    {
        SceneManager.LoadSceneAsync((int)gameScene);
    }
}
