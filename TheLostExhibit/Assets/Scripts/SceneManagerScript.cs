using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject controlsPanel; // Assign in inspector
    public GameObject creditsPanel;  // Assign in inspector
    public Canvas transitionCanvas;  // Assign in inspector
    public float transitionDuration = 3.0f; // Duration for transition images

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For testing in Editor
#endif
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(TransitionAndLoadScene(sceneName));
    }

    private IEnumerator TransitionAndLoadScene(string sceneName)
    {
        if (transitionCanvas != null)
        {
            transitionCanvas.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(transitionDuration);

        if (transitionCanvas != null)
        {
            transitionCanvas.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    LoadScene("MuseumRoomScene");
                }
                else if (hit.collider.CompareTag("Artifact"))
                {
                    LoadSceneWithTransition("Level_1");
                }
                else if (hit.collider.CompareTag("NPC"))
                {
                    LoadSceneWithTransition("Level_2");
                }
            }
        }
    }
}

