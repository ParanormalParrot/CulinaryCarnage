using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;
    private void Start()
    {
        SoundManager.instance.LoadSettings();
        SoundManager.instance.PlayMenuMusic();
        MainMenu.SetActive(true);
        Options.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("DinerMain", LoadSceneMode.Single);
        Options.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);
        SoundManager.instance.LoadSettings();
    }


    public void Back()
    {
        SoundManager.instance.SaveSettings();
        MainMenu.SetActive(true);
        Options.SetActive(false);
    }
}