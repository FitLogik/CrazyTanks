using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderVolumeMusic;
    [SerializeField] private Slider sliderVolumeSFX;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") )
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = sliderVolumeMusic.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PrefsManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume()
    {
        float volume = sliderVolumeSFX.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PrefsManager.SetSFXVolume(volume);
    }

    private void LoadVolume()
    {
        sliderVolumeMusic.value = PrefsManager.GetMusicVolume();
        sliderVolumeSFX.value = PrefsManager.GetSFXVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
