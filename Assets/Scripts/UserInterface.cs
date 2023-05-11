using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UserInterface : MonoBehaviour
{
    public static UserInterface instance;
    public DisplayInventory inventory;
    public CookingMenu cookingMenu;
    public BossHealthbar bossHealthbar;
    public GameObject victoryMenu;
    public GameObject defeatMenu;
    public GameObject pauseMenu;
    public GameObject transition;
    public bool inventoryOpen;
    public bool pauseMenuOpen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
        inventory.GameObject().SetActive(false);
        bossHealthbar.GameObject().SetActive(false);
        victoryMenu.SetActive(false);
        defeatMenu.SetActive(false);
        pauseMenu.SetActive(false);
        transition.SetActive(false);
        inventoryOpen = false;
        pauseMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.GameObject().SetActive(!inventory.GameObject().activeSelf);
            cookingMenu.GameObject().SetActive(!cookingMenu.GameObject().activeSelf);
            inventoryOpen = !inventoryOpen;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuOpen = !pauseMenuOpen;
            inventory.GameObject().SetActive(false);
            cookingMenu.GameObject().SetActive(false);
            pauseMenu.GameObject().SetActive(!pauseMenu.GameObject().activeSelf);
            if (pauseMenuOpen)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }


    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }


    public void Resume()
    {
        pauseMenuOpen = false;
        pauseMenu.GameObject().SetActive(false);
        Time.timeScale = 1;
    }

    public void StartTransition()
    {
        StartCoroutine(LevelTransition());
    }

    IEnumerator LevelTransition()
    {
        Time.timeScale = 1;
        transition.SetActive(true);
        transition.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Diner - Floor " + LevelGenerator.instance.currentFloorNumber;
        yield return new WaitForSeconds(2f);
        transition.SetActive(false);
    }
}