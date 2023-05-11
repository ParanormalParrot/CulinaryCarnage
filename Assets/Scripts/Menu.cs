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

    // Запуск игровой сессии
    public void Play()
    {
        SceneManager.LoadScene("DinerMain", LoadSceneMode.Single);
        Options.SetActive(false);
    }

    // Выход из игры
    public void Quit()
    {
        Application.Quit();
    }

    // Открыть настройки
    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);
        SoundManager.instance.LoadSettings();
    }


    // Выход из меню настроек
    public void Back()
    {
        SoundManager.instance.SaveSettings();
        MainMenu.SetActive(true);
        Options.SetActive(false);
    }
}