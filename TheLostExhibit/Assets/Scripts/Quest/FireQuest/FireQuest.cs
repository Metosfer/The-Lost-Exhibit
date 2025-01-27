using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireQuest : MonoBehaviour
{
    public Camera mainCamera;

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

    private bool isRayDrawing = false;  // ���� ��� ��������� ����
    private float rayDuration = 0.1f;  // ������������ ��������� ����

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        failPanel.SetActive(false);
        winImage.SetActive(false);
        startFlag = false;
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        startImage.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        startFlag = true;
        startImage.SetActive(false);
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("������ �� ��������� � ����������!");
            return;
        }

        if (Input.GetMouseButtonDown(0) && startFlag)
        {
            // ���������� ��� ������ ���� ���� isRayDrawing true
            if (!isRayDrawing)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, rayDuration);  // ������ ��� � ������������ �������� �����
                isRayDrawing = true;  // ������������� ����, ����� �� �������� ��������� �����
            }

            Ray rayForHit = mainCamera.ScreenPointToRay(Input.mousePosition);

            // �������� ��� �������, ������� ���������� ���
            RaycastHit[] hits = Physics.RaycastAll(rayForHit);

            // �������� �� ���� ������������ ��������
            foreach (var hit in hits)
            {
                // ���������, ���� ������ �� swapObjects
                if (swapObjects.Contains(hit.collider.gameObject))
                {
                    HandleObjectSelection(hit.collider.gameObject);
                    break; // ��������� ���� ����� ���������� ������� ����������� �������
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
            Debug.LogError("���������� ���������� ������� �� ��������� � ����������� ��������!");
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
            SceneManager.LoadScene(8);
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

            // ���������, ��� ��� ������� �� ������ swapObjects
            if (swapObjects.Contains(firstSelected) && swapObjects.Contains(secondSelected))
            {
                SwapPositions();
            }
            else
            {
                // ���� ���� �� ���� �� �������� �� �� ������ swapObjects, �������� ���������
                firstSelected.GetComponent<OutlineCustom>().enabled = false;
                firstSelected = null;
                secondSelected = null;
            }
        }
    }

    private void SwapPositions()
    {
        if (firstSelected != null && secondSelected != null)
        {
            // ������ ������� ��������
            Vector3 tempPosition = firstSelected.transform.position;
            firstSelected.transform.position = secondSelected.transform.position;
            secondSelected.transform.position = tempPosition;

            // ��������� ���������
            firstSelected.GetComponent<OutlineCustom>().enabled = false;

            // �������� ������
            firstSelected = null;
            secondSelected = null;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
