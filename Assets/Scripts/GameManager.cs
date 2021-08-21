using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMode gameMode = GameMode.NORMAL;
    public ushort BestScore { get => CurrentStage.bestScore; set => CurrentStage.bestScore = value; }
    public float Volume { get => _volume; private set => _volume = value; }
    public StageSO CurrentStage { get; set; }

    private float _volume = 0.5f;
    private float _defaultVolume = 0.5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        Volume = _defaultVolume;
    }

    //Game mode (enum) setter
    public void SetGameMode(int mode)
    {
        gameMode = (GameMode)mode;
    }

    //Game mode (enum) getter
    public GameMode GetGameMode()
    {
        return gameMode;
    }

    //Volume setter
    public void SetVolume(float value)
    {
        Volume = value;
    }

    //Unpausing the game and setting time to normal
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    //Loads main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public enum GameMode { EASY, NORMAL, HARD };
