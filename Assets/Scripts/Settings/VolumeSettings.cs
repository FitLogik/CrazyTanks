using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] Slider sliderVolumeMusic;
    [SerializeField] Slider sliderVolumeSFX;


    private void Start()
    {
        Init();
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.MusicVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.SFXVolume = value;
    }

    private void Init()
    {
        float musicVolume = AudioManager.MusicVolume;
        float sfxVolume = AudioManager.SFXVolume;

        UpdateUI(musicVolume, sfxVolume);
    }

    private void UpdateUI(float musicVolume, float sfxVolume)
    {
        sliderVolumeMusic.value = musicVolume;
        sliderVolumeSFX.value = sfxVolume;
    }
}
