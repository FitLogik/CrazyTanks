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
    private bool isSettings = false;
    private bool isSettingsPanel = false;
    private bool isLevel = false;
    private bool isColor = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Можно использовать любую другую клавишу или условие
        {
            if (true)
            {


                if (isSettings)
                {
                    CloseSettingsMenu(); // Закрыть настройки
                }
                else
                {
                    Application.Quit(); // Закрыть приложение
                }
            }
        }
    }


public void LoadGameLevel(string level)
    {
        Instantiate(gameManagerPrefab);
        if (Convert.ToInt32(level) == 1 || Convert.ToInt32(level) == 3 || Convert.ToInt32(level) == 5)
        {
            GameManager.GameType = GameType.Game1Player;
        }
        else
        {
            GameManager.GameType = GameType.Game1PlayerWithBot;
        }
        string sceneName = "GameLevel" + level;
        GameManager.LoadScene1Player(sceneName);
    }
    

    public void LoadGame2Players()
    {
        // Создаем объект GameManager (при создании, к нему применяется DontDestroyOnLoad)
        Instantiate(gameManagerPrefab);
        GameManager.GameType = GameType.Game2Players;
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
        isSettingsPanel = true;
    }

    public void CloseSettingsMenu()
    {
        SaveSettings();
        if (isSettings)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            isSettingsPanel = false;
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
        isColor = true;
    }

    public void CloseColorMenu()
    {
        SaveColors();
        mainMenu.SetActive(true);
        colorMenu.SetActive(false);
        isColor = false;    
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
        isLevel = true;
    }

    public void CloseLevelMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
        isLevel = false;
    }
}
