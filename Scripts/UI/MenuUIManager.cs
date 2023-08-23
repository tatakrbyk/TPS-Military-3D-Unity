using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("All Menu's")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject endGameMenuUI;
    [SerializeField] private GameObject objectivesMenuUI;

    public static bool GameIsStoopped = false;

    private void Start()
    {
        objectivesMenuUI.SetActive(true);
        objectivesMenuUI.SetActive(false);
    }
    private void Update()
    {
        PauseController();
    }

    private void PauseController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStoopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;

            }
        }else if(Input.GetKeyDown("m"))
        {
            if(GameIsStoopped)
            {
                removeObjectivesUI();
                Cursor.lockState = CursorLockMode.Locked;

            }
        else
            {
                showObjectivesUI();
                Cursor.lockState = CursorLockMode.None;

            }
        }

    }

    public void showObjectivesUI()
    {
        objectivesMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStoopped = true;
    }

    public void removeObjectivesUI()
    {
        objectivesMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStoopped = false;
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStoopped = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Mission");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1.0f;
        GameIsStoopped=true;
        
    }
}
