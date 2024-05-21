using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioMixer audioMixer;

    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip shot1;
    public AudioClip shot2;
    public AudioClip shot3;
    public AudioClip bonusPickUp;
    public AudioClip hit;
    public AudioClip hitGround;
    public AudioClip winRound;
    public AudioClip winGame;
    public AudioClip death;
    public AudioClip wind;
    public AudioClip click1;
    public AudioClip click2;
    public AudioClip click3;

    public static float MusicVolume
    {
        get => Instance._musicVolume;
        set => SetMusicVolume(value);
    }
    public static float SFXVolume
    {
        get => Instance._sfxVolume;
        set => SetSFXVolume(value);
    }

    float _musicVolume;
    float _sfxVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        InitSettings();
        LoadSettings();
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private void InitSettings()
    {
        _musicVolume = PrefsManager.GetMusicVolume();
        _sfxVolume = PrefsManager.GetSFXVolume();
    }

    private void LoadSettings()
    {
        SetMusicVolume(_musicVolume);
        SetSFXVolume(_sfxVolume);
    }

    private static void SetMusicVolume(float level)
    {
        Instance.audioMixer.SetFloat("music", Mathf.Log10(level) * 20);
        Instance._musicVolume = level;
    }

    private static void SetSFXVolume(float level)
    {
        Instance.audioMixer.SetFloat("SFX", Mathf.Log10(level) * 20);
        Instance._sfxVolume = level;
    }

    internal static void SaveSettings()
    {
        PrefsManager.SetMusicVolume(MusicVolume);
        PrefsManager.SetSFXVolume(SFXVolume);
    }

    public void PlaySFXClick()
    {
        PlaySFX(click2);
    }
}
