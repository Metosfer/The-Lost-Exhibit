using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;       // The main pause menu panel
    public GameObject confirmationPanel; // The confirmation panel
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        confirmationPanel.SetActive(false); // Ensure confirmation panel is hidden
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenConfirmationPanel()
    {
        confirmationPanel.SetActive(true);
        pauseMenuUI.SetActive(false); // Hide the pause menu
    }

    public void CloseConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
        pauseMenuUI.SetActive(true); // Show the pause menu again
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before changing scene
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For testing in the Editor
#endif
    }
}






