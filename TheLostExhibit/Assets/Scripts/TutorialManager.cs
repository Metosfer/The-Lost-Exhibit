using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    private bool hasCollectedTicket = false;
    private bool isTicketDropped = false;
    private bool isPlayerNearDoor = false;
    public Transform dropPoint;
    public GameObject ticketPrefab;
    public GameObject markedAreaPrefab; // Inspector'dan s�r�kleyip b�rak�n
    public TextMeshProUGUI reminderText;

    void Start()
    {
        if (tutorialText == null)
        {
            tutorialText = GetComponent<TextMeshProUGUI>();
        }

        // Ba�lang��ta i�aretli alan� gizle
        if (markedAreaPrefab != null)
        {
            markedAreaPrefab.SetActive(false);
        }

        UpdateTutorialText();
    }

    void Update()
    {
        if (hasCollectedTicket && Input.GetKeyDown(KeyCode.Alpha1) && !isTicketDropped)
        {
            DropTicket();
        }

        if (isPlayerNearDoor && Input.GetKeyDown(KeyCode.E) && isTicketDropped)
        {
            Debug.Log("Trying to enter museum...");
            TryEnterMuseum();
        }
    }

    public void SetPlayerNearDoor(bool isNear)
    {
        isPlayerNearDoor = isNear;
        UpdateTutorialText();
    }

    public void OnTicketCollected()
    {
        hasCollectedTicket = true;
        // Ticket al�nd���nda i�aretli alan� g�ster
        if (markedAreaPrefab != null)
        {
            markedAreaPrefab.SetActive(true);
        }
        UpdateTutorialText();
    }

    private void DropTicket()
    {
        Instantiate(ticketPrefab, dropPoint.position, Quaternion.identity);
        isTicketDropped = true;
        // Ticket b�rak�ld���nda i�aretli alan� gizle
        if (markedAreaPrefab != null)
        {
            markedAreaPrefab.SetActive(false);
        }
        UpdateTutorialText();
    }

    private void TryEnterMuseum()
    {
        SceneManager.LoadScene(2);
    }

    private void UpdateTutorialText()
    {
        if (!hasCollectedTicket)
        {
            tutorialText.text = "Press E to take your ticket";
            reminderText.text = "If you stuck any point, press R to reset the level!!!";
        }
        else if (hasCollectedTicket && !isTicketDropped)
        {
            tutorialText.text = "Go to the marked area with W, A, S, D keys and press 1 to drop the ticket (You can rotate with Mouse)";
        }
        else if (isTicketDropped)
        {
            tutorialText.text = "Press E to interact with the door and enter the museum";
            
        }
    }
}