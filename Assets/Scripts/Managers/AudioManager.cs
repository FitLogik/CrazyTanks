using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip shot1;
    public AudioClip shot2;
    public AudioClip shot3;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip wind;
    public AudioClip click1;
    public AudioClip click2;
    public AudioClip click3;




    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
