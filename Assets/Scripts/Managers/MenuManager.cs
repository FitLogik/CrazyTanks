using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: давай по новой, Миша, всё...

    public string game1PlayerSceneName;

    public GameObject gameManagerPrefab;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject settingsPanel;
    public GameObject colorMenu;
    public GameObject levelMenu;
    public GameObject controlInformationMenu;
    public GameObject buttonInformation;
    private bool isSettings;

    public void LoadGameLevel(string level)
    {
        Instantiate(gameManagerPrefab);
        GameManager.GameType = GameTypes.Game1Player;
        string sceneName = "GameLevel" + level;
        GameManager.LoadScene1Player(sceneName);
    }
    

    public void LoadGame2Players()
    {
        // Создаем объект GameManager (при создании, к нему применяется DontDestroyOnLoad)
        Instantiate(gameManagerPrefab);
        GameManager.GameType = GameTypes.Game2Players;
        GameManager.LoadRandomScene();
    }

    public void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsPanel.SetActive(true);
        controlInformationMenu.SetActive(false);   
        buttonInformation.SetActive(true);
        isSettings = true;
    }

    public void CloseSettingsMenu()
    {
        SaveSettings();
        if (isSettings)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
            controlInformationMenu.SetActive(false);
            buttonInformation.SetActive(true);
            isSettings = true;
        }
    }

    public void OpenGuidePanel()
    {
        isSettings = false;
        settingsPanel.SetActive(false);
        controlInformationMenu.SetActive(true);
        buttonInformation.SetActive(false);
    }

    public void OpenColorMenu()
    {
        mainMenu.SetActive(false);
        colorMenu.SetActive(true);
    }

    public void CloseColorMenu()
    {
        SaveColors();
        mainMenu.SetActive(true);
        colorMenu.SetActive(false);
    }

    private void SaveColors()
    {
        ColorManager.SaveColors();
    }

    private void SaveSettings()
    {
        AudioManager.SaveSettings();
    }

    public void OpenLevelMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);

    }

    public void CloseLevelMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }
}
