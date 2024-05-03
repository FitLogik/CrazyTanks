using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameTypes
    {
        Game1Player = 1,
        Game2Players = 2
    }

    private GameTypes game;

    [SerializeField] SceneAsset[] scenes2Players;


    public int Player1Score { get; set; }
    public int Player2Score { get; set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // TODO: Временное решение
        game = GameTypes.Game2Players;
    }

    
    public static void LoadRandomScene()
    {
        if (instance.game == GameTypes.Game2Players)
        {
            int scenesCount = instance.scenes2Players.Length;
            if (scenesCount > 0)
            {
                SceneAsset randomScene = instance.scenes2Players[UnityEngine.Random.Range(0, scenesCount)];
                string randomSceneString = randomScene.name;
                SceneManager.LoadSceneAsync(randomSceneString);
            }
        }
    }
}
