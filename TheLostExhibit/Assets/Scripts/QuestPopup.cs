using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : MonoBehaviour
{
    public GameObject questPanel; // UI Panel
    public Button nextButton; // Next Butonu
    private bool isPlayerNearby = false; // Oyuncu NPC'ye yakın mı?

    void Start()
    {
        questPanel.SetActive(false); // Panel başlangıçta kapalı
        nextButton.onClick.AddListener(ClosePanel); // Butona tıklayınca kapatma
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // NPC'ye yaklaşınca E'ye basılırsa aç
        {
            OpenPanel();
        }

        if (questPanel.activeSelf && Input.GetKeyDown(KeyCode.Return)) // Panel açıksa ve Enter'a basılırsa kapat
        {
            ClosePanel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu NPC'ye yaklaştı mı?
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu NPC'den uzaklaştı mı?
        {
            isPlayerNearby = false;
        }
    }

    public void OpenPanel()
    {
        questPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        questPanel.SetActive(false);
    }
}
