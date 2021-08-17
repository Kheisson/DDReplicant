using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelector : MonoBehaviour
{
    private TextMeshProUGUI _songName;
    private string[] _songList = { "Song 1", "Song 2", "Song 3" };
    private byte _songNumber = 1;

    public Button nextButton;
    public Button prevButton;

    private void Start()
    {
        _songName = GetComponent<TextMeshProUGUI>();
        _songName.text = _songList[1];
    }

    public void NextItem()
    {
        prevButton.interactable = true;
        _songName.text = _songList[++_songNumber];
        if (_songNumber == _songList.Length - 1)
        {
            nextButton.interactable = false;
        }
    }

        public void PrevItem()
    {
        nextButton.interactable = true;
        _songName.text = _songList[--_songNumber];
        if (_songNumber == 0)
        {
            prevButton.interactable = false;
        }
    }


}
