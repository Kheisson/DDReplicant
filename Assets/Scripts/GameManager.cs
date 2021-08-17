using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMode gameMode = GameMode.NORMAL;
    public ushort BestScore { get => _bestScore; set => _bestScore = value; }
    public float Volume { get => _volume; private set => _volume = value; }

    private float _volume = 0.5f;
    private float _defaultVolume = 0.5f;
    private ushort _bestScore;

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

    public void SetGameMode(int mode)
    {
        gameMode = (GameMode)mode;
    }

    public GameMode GetGameMode()
    {
        return gameMode;
    }

    public void SetVolume(float value)
    {
        Volume = value;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
public enum GameMode { EASY, NORMAL, HARD };