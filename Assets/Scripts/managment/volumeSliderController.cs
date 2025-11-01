using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);

        float savedVolume = PlayerPrefs.GetFloat("masterVolume", 1f);
        volumeSlider.value = savedVolume;
        SetMasterVolume(savedVolume);
    }


    void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
    }

    void SetMasterVolume(float volume)
    {
        GameManager.Instance.MusicSource.volume = volume;
        GameManager.Instance.SFXSource.volume = volume;

        PlayerPrefs.SetFloat("masterVolume", volume);
    }
}
