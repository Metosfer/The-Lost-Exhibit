using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Event system için gerekli

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject confirmationPanel;
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
        // UI elementlerinin highlight durumunu sýfýrla
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        pauseMenuUI.SetActive(false);
        confirmationPanel.SetActive(false);
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
        pauseMenuUI.SetActive(false);
    }

    public void CloseConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}