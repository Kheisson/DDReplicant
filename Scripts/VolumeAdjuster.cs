using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VolumeAdjuster : MonoBehaviour
{
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = GameManager.Instance.Volume;
    }

    public void ChangeVolume(float value)
    {
        GameManager.Instance.SetVolume(value);
    }
}
