using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject museumPausePanel; // Assign the MuseumScene pause panel in the Inspector
    public GameObject defaultPausePanel; // Assign the default pause panel for other scenes in the Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Pause the game

        string currentScene = SceneManager.GetActiveScene().name;

        // Show the appropriate pause menu
        if (currentScene == "MuseumScene")
        {
            museumPausePanel.SetActive(true);
        }
        else
        {
            defaultPausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Resume the game

        // Hide both panels
        museumPausePanel.SetActive(false);
        defaultPausePanel.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1; // Resume time before quitting
        SceneManager.LoadScene("Menu");
    }
}



