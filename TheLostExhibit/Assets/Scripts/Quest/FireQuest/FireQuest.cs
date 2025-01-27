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

    private bool isRayDrawing = false;  // Флаг для рисования луча
    private float rayDuration = 0.1f;  // Длительность видимости луча

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
            Debug.LogWarning("Камера не назначена в инспекторе!");
            return;
        }

        if (Input.GetMouseButtonDown(0) && startFlag)
        {
            // Отображаем луч только если флаг isRayDrawing true
            if (!isRayDrawing)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, rayDuration);  // Рисуем луч с ограниченным временем жизни
                isRayDrawing = true;  // Устанавливаем флаг, чтобы не рисовать несколько лучей
            }

            Ray rayForHit = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Получаем все объекты, которые пересекает луч
            RaycastHit[] hits = Physics.RaycastAll(rayForHit);

            // Проходим по всем пересеченным объектам
            foreach (var hit in hits)
            {
                // Проверяем, если объект из swapObjects
                if (swapObjects.Contains(hit.collider.gameObject))
                {
                    HandleObjectSelection(hit.collider.gameObject);
                    break; // Прерываем цикл после нахождения первого подходящего объекта
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

            // Проверяем, что оба объекта из списка swapObjects
            if (swapObjects.Contains(firstSelected) && swapObjects.Contains(secondSelected))
            {
                SwapPositions();
            }
            else
            {
                // Если хотя бы один из объектов не из списка swapObjects, отменяем выделение
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
            // Меняем позиции объектов
            Vector3 tempPosition = firstSelected.transform.position;
            firstSelected.transform.position = secondSelected.transform.position;
            secondSelected.transform.position = tempPosition;

            // Отключаем выделение
            firstSelected.GetComponent<OutlineCustom>().enabled = false;

            // Обнуляем ссылки
            firstSelected = null;
            secondSelected = null;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
