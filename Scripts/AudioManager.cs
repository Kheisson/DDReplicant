using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip clipOne;

    public delegate void SongEnded();
    public event SongEnded OnSongEnded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource.volume = GameManager.Instance.Volume;
    }

    private void Update()
    {
        if (audioSource.clip.length + 2f <= Time.timeSinceLevelLoad)
        {
            OnSongEnded?.Invoke();
            SpawnManager.Spawner.StopAllCoroutines();
        }
    }

    [ContextMenu("TestSongEnd")]
    private void TestSongEnd()
    {
        audioSource.Stop();
        OnSongEnded?.Invoke();
    }


}
