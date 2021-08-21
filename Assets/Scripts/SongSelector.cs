using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelector : MonoBehaviour
{
    private TextMeshProUGUI _songName;
    private string[] _songList = { "Neon Volt", "Tanhum", "Take the Shot" };
    private byte _songNumber = 1;
    private AudioSource _audioSource;

    public Button nextButton;
    public Button prevButton;
    public StageSO[] songs;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _songName = GetComponent<TextMeshProUGUI>();
        _songName.text = _songList[1];
        _audioSource.clip = songs[_songNumber].previewClip;
        PlaySelectedSong(1.5f);
        GameManager.Instance.CurrentStage = songs[_songNumber];
    }

    //Adavances the song list to the next item
    public void NextItem()
    {
        prevButton.interactable = true;
        _songName.text = _songList[++_songNumber];
        if (_songNumber == _songList.Length - 1)
        {
            nextButton.interactable = false;
        }
        _audioSource.clip = songs[_songNumber].previewClip;
        PlaySelectedSong(0.5f);
        GameManager.Instance.CurrentStage = songs[_songNumber];
    }

    //Moves song list to the previous (left) item
        public void PrevItem()
    {
        nextButton.interactable = true;
        _songName.text = _songList[--_songNumber];
        if (_songNumber == 0)
        {
            prevButton.interactable = false;
        }
        _audioSource.clip = songs[_songNumber].previewClip;
        PlaySelectedSong(0.5f);
        GameManager.Instance.CurrentStage = songs[_songNumber];
    }

    //Play selected song with delay
    private void PlaySelectedSong(float delay)
    {
        _audioSource.PlayDelayed(delay);
    }
}
