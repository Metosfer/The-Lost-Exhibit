using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject controlsPanel; // Assign in inspector
    public GameObject creditsPanel;  // Assign in inspector

    public QuestGiver questGiver; // Assign the NPC's QuestGiver script in the Inspector

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
                    LoadScene("Level_1");
                }
                else if ((hit.collider.CompareTag("NPC")) && (questGiver != null && questGiver.Helped))
                {
                    LoadScene("InBetweenScene");
                     
                }
                else
                {
                    Debug.Log("You need to complete the quest first!");
                }
            }
        }
    }
}



