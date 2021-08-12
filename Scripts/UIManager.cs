using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI _textTMP;
    private ushort _score = 0;
    private Slider volumeSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI personalBestText;
    /*
     * Below are string for scoring, because strings are immutable, we shouldn't
     * Create new ones everytime to save un needed GC
     */
    private string _excellentScoreString = "Excellent";
    private string _greatScoreString = "Great";
    private string _goodScoreString = "Good";
    private string _badScoreString = "Bad";

    [SerializeField] private TextMeshProUGUI performanceGague;
    [SerializeField] private GameObject pauseMenu;

    public static UIManager Instance;
    public GameObject gameOverUI;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.OnSongEnded += PopupGameOverUI;
        _textTMP = GetComponentInChildren<TextMeshProUGUI>();
        performanceGague.text = "";
        volumeSlider = pauseMenu.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }
    }

    //Updates score and gauge based on score
    public void UpdateScoreText(int amount)
    {
        ushort highScore = GameManager.Instance.BestScore;
        _score += (ushort)amount;
        if (_score > highScore)
            GameManager.Instance.BestScore = _score;
        _textTMP.text = $"  Score: {_score}";
        switch(amount)
        {
            case 100: performanceGague.text = _excellentScoreString;
                performanceGague.color = Color.green;
                break;
            case 85:  performanceGague.text = _greatScoreString;
                performanceGague.color = Color.blue;
                break;
            case 65: performanceGague.text = _goodScoreString;
                performanceGague.color = Color.yellow;
                break;
            default: performanceGague.text = _badScoreString;
                performanceGague.color = Color.red;
                break;
        }
    }

    //Subscribed to audio event, pops up gameover screen
    private void PopupGameOverUI()
    {
        performanceGague.text = "";
        scoreText.text = "Score: " + _score.ToString();
        personalBestText.text = "Personal Best:" + GameManager.Instance.BestScore;
        gameOverUI.SetActive(true);
    }

    //Handles actions related to the pause menu
    private void HandlePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        if (AudioManager.Instance.audioSource.isPlaying == true)
        {
            AudioManager.Instance.audioSource.Pause();
            volumeSlider.value = GameManager.Instance.Volume;
        }
        else
        {
            AudioManager.Instance.audioSource.UnPause();
            GameManager.Instance.SetVolume(volumeSlider.value);
        }
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
