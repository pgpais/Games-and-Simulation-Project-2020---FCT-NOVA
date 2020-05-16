using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu Instance;
    public GameObject pauseMenu;
    
    public bool GameIsPaused { get; private set; }

    public GameObject analyticsUi;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public bool IsPaused()
    {
        return GameIsPaused;
    }

    public void MenuTrigger()
    {
        if (IsPaused())
        {
            Resume();
        } else
        {
            Pause();
        }
    }
    
    void Resume()
    {
        pauseMenu.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Since we do not want to pause the time yet
        // We'll comment this line of code
        // Time.timeScale = 1f;
        
        GameIsPaused = false;

    }
    
    void Pause()
    {
        pauseMenu.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Since we do not want to pause the time yet
        // We'll comment this line of code
        // Time.timeScale = 0f;

        GameIsPaused = true;
    }
    
    public void ToggleAnalytics()
    {
        analyticsUi.SetActive(!analyticsUi.activeSelf);
    }
    
}
