using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: давай по новой, Миша, всё...

    public GameObject gameManagerPrefab;

    public GameObject mainMenu;
    public GameObject settingsMenu;


    public enum GameScenes
    {
        Game1Player = 0,
        Game2Players = 3
    }

    public void LoadGame1Player()
    {
        LoadScene(GameScenes.Game1Player);
    }

    public void LoadGame2Players()
    {
        LoadScene(GameScenes.Game2Players);
    }

    public void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        SaveSettings();
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    private void SaveSettings()
    {
        AudioManager.SaveSettings();
    }

    public void LoadScene(GameScenes gameScene)
    {
        // Создаем объект GameManager (при создании, к нему применяется DontDestroyOnLoad)
        Instantiate(gameManagerPrefab);

        GameManager.LoadRandomScene();
    }
}
