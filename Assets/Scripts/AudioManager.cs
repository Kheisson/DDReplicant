using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;

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

        if (GameManager.Instance.CurrentStage.fullTrack != null)
            audioSource.clip = GameManager.Instance.CurrentStage.fullTrack;
    }

    private void Start()
    {
        audioSource.volume = GameManager.Instance.Volume;
        audioSource.PlayDelayed(0.5f);
    }

    private void Update()
    {
        if (audioSource.clip.length + 2f <= Time.timeSinceLevelLoad)
        {
            SpawnManager.Spawner.StopAllCoroutines();
            OnSongEnded?.Invoke();
        }
    }

    //Testing end song mechanics 
    [ContextMenu("TestSongEnd")]
    private void TestSongEnd()
    {
        audioSource.Stop();
        OnSongEnded?.Invoke();
    }


}
