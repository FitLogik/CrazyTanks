using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // TODO: ����� �� �����, ����, ��...

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
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void LoadScene(GameScenes gameScene)
    {
        // ������� ������ GameManager (��� ��������, � ���� ����������� DontDestroyOnLoad)
        GameManager gameManager = Instantiate(gameManagerPrefab).AddComponent<GameManager>();

        GameManager.LoadRandomScene();
    }
}
