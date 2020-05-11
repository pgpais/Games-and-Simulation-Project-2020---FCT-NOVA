using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu instance;
    
    private static bool _gameIsPaused;

    public GameObject pauseMenuUI;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public bool isPaused()
    {
        return _gameIsPaused;
    }

    public void MenuTrigger()
    {
        if (isPaused())
        {
            Resume();
        } else
        {
            Pause();
        }
    }
    
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Since we do not want to pause the time yet
        // We'll comment this line of code
        // Time.timeScale = 1f;
        
        _gameIsPaused = false;

    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Since we do not want to pause the time yet
        // We'll comment this line of code
        // Time.timeScale = 0f;

        _gameIsPaused = true;
    }
    
}
