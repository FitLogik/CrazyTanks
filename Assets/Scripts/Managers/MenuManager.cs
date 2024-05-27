using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string game1PlayerSceneName;

    public GameObject gameManagerPrefab;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject settingsPanel;
    public GameObject colorMenu;
    public GameObject levelMenu;
    public GameObject controlInformationMenu;
    public GameObject buttonInformation;
    private bool isGuide = false;
    private bool isSettings = false;
    private bool isLevel = false;
    private bool isColor = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettings || isGuide)
            {
                CloseSettingsMenu(); // ������� ���������
            }
            else if (isLevel)
            {
                CloseLevelMenu();    // ������� ����� �������
            }
            else if (isColor)
            {
                CloseColorMenu();    // ������� ����� �����
            }
            else
            {
                Application.Quit();  // ������� ����������
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
        // ������� ������ GameManager (��� ��������, � ���� ����������� DontDestroyOnLoad)
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
        isGuide = false;
    }

    public void CloseSettingsMenu()
    {
        SaveSettings();
        if (isSettings)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            isSettings = false;
        }
        else
        {
            settingsPanel.SetActive(true);
            controlInformationMenu.SetActive(false);
            buttonInformation.SetActive(true);
            isSettings = true;
            isGuide = false;
        }
    }

    public void OpenGuidePanel()
    {
        isSettings = false;
        isGuide = true;
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
