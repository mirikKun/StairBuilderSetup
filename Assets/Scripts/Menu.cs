using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string startMenu = "StartMenu";
    private bool _gameInPaused;
    private bool _gameEnded;
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || _gameEnded) return;
        if (_gameInPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        _gameInPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        _gameInPaused = false;
        
    }
    public void Victory()
    {
        victoryMenu.SetActive(true);
        _gameEnded = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(startMenu);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
