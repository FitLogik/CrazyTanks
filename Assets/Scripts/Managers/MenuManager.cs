using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: ����� �� �����, ����, ��...

    public GameObject gameManagerPrefab;

    public GameObject mainMenu;
    public GameObject settingsMenu;



    public void LoadGame1Player()
    {
        Debug.LogError("�� �����������!");
    }

    public void LoadGame2Players()
    {
        // ������� ������ GameManager (��� ��������, � ���� ����������� DontDestroyOnLoad)
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
