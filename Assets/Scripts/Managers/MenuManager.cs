using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: давай по новой, Миша, всё...

    public GameObject gameManagerPrefab;

    public GameObject mainMenu;
    public GameObject settingsMenu;



    public void LoadGame1Player()
    {
        Debug.LogError("Не реализовано!");
    }

    public void LoadGame2Players()
    {
        // Создаем объект GameManager (при создании, к нему применяется DontDestroyOnLoad)
        Instantiate(gameManagerPrefab);

        GameManager.LoadRandomScene();
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
}
