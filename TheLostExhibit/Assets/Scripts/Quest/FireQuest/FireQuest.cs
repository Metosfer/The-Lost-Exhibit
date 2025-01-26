using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireQuest : MonoBehaviour
{
    
    public List<GameObject> swapObjects;

    
    private GameObject firstSelected = null;
    private GameObject secondSelected = null;


    public GameObject failPanel;
    [TextArea] public string failText;

    public GameObject startImage;
    public GameObject winImage;

    public TextMeshProUGUI text;
    private bool startFlag;



    
    public List<Vector3> correctPositions;

    private void Start()
    {

        failPanel.SetActive(false);
        winImage.SetActive(false);
        startFlag = false;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        startImage.SetActive(true);
        yield return new WaitForSeconds(2);
        startImage.SetActive(false);
        startFlag = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && startFlag) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (swapObjects.Contains(hit.collider.gameObject)) 
                {
                    HandleObjectSelection(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            CheckOrder(); 
        }
    }

    private void CheckOrder()
    {
        if (correctPositions.Count != swapObjects.Count)
        {
            Debug.LogError("Количество правильных позиций не совпадает с количеством объектов!");
            return;
        }

        bool isCorrect = true;

        for (int i = 0; i < swapObjects.Count; i++)
        {
            if (swapObjects[i].transform.position != correctPositions[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            winImage.SetActive(true);
            startFlag = false;

        }
        else
        {
            text.text = failText;
            failPanel.SetActive(true);
            startFlag = false;
        }
    }

    private void HandleObjectSelection(GameObject selectedObject)
    {
        if (firstSelected == null)
        {
            firstSelected = selectedObject;
            firstSelected.GetComponent<OutlineCustom>().enabled = true;
        }
        else if (secondSelected == null && selectedObject != firstSelected)
        {
            secondSelected = selectedObject;

            SwapPositions(); 
        }
    }

    private void SwapPositions()
    {
        if (firstSelected != null && secondSelected != null)
        {
            
            Vector3 tempPosition = firstSelected.transform.position;
            firstSelected.transform.position = secondSelected.transform.position;
            secondSelected.transform.position = tempPosition;

            firstSelected.GetComponent<OutlineCustom>().enabled = false;

            
            firstSelected = null;
            secondSelected = null;
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
