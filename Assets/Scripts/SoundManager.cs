using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;

    public float volume;
    public AudioClip menuMusic;

    public Slider slider;

    void Awake()
    {
        volume = 1;
        slider.value = 1;
        audioSource.volume = volume;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    


    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }


    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
    }
    
    
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    public void LoadSettings()
    {
        slider = FindObjectOfType<Slider>();
        slider.onValueChanged.AddListener(
            delegate { ChangeVolume(); });
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
    


}