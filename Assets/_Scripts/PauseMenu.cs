using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private PlayerControls controls;
    private bool isPaused = false;

    void Awake()
    {
        // Initialize PlayerControls instance
        controls = new PlayerControls();
        controls.Enable();

        // Ensure the pause menu is initially hidden
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause menu UI is not assigned in the inspector!"); // Add error log
        }
    }

    void OnEnable()
    {
        // Enable input actions
        if (controls != null)
        {
            controls.Player.Enable();
        }
        else
        {
            Debug.LogError("PlayerControls is not initialized!"); // Add error log
        }
    }

    void OnDisable()
    {
        // Disable input actions
        if (controls != null)
        {
            controls.Player.Disable();
        }
        else
        {
            Debug.LogError("PlayerControls is not initialized!"); // Add error log
        }
    }

    void Update()
    {
        // Check if the pause action is triggered
        if (controls != null && controls.Player.Pause.triggered)
        {
            Debug.Log("Pause action triggered!"); // Add debug log
            TogglePause();
        }
    }

void TogglePause()
{
    // Toggle pause state
    isPaused = !isPaused;

    // Pause or resume the game
    Time.timeScale = isPaused ? 0f : 1f;
    Debug.Log("Game paused: " + isPaused); // Add debug log

    // Show or hide the pause menu UI
    if (pauseMenuUI != null)
    {
        pauseMenuUI.SetActive(isPaused);
        Debug.Log("Pause menu visible: " + isPaused); // Add debug log

        // Set cursor visibility and lock state based on pause state
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
}