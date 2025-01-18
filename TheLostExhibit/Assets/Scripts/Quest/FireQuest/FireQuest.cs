using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireQuest : MonoBehaviour
{
    // Список объектов, которыми можно взаимодействовать
    public List<GameObject> swapObjects;

    // Хранит выбранные объекты
    private GameObject firstSelected = null;
    private GameObject secondSelected = null;

    public GameObject startImage;
    public GameObject winImage;

    public TextMeshProUGUI text;
    private bool startFlag;



    // Список правильных позиций объектов (заполняется вручную)
    public List<Vector3> correctPositions;

    private void Start()
    {
        winImage.SetActive(false);
        startFlag = false;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        startImage.SetActive(true);
        yield return new WaitForSeconds(4);
        startImage.SetActive(false);
        startFlag = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && startFlag) // Проверка клика левой кнопкой мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Луч из камеры в точку клика
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Проверка на попадание в объект
            {
                if (swapObjects.Contains(hit.collider.gameObject)) // Проверка, что объект в списке
                {
                    HandleObjectSelection(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Проверка нажатия клавиши Enter
        {
            CheckOrder(); // Проверка правильного порядка объектов
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
            text.text = "Loose";
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

            SwapPositions(); // Меняем местами
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

            firstSelected.GetComponent<OutlineCustom>().enabled = false;

            // Сбрасываем выбор
            firstSelected = null;
            secondSelected = null;
        }
    }
}
