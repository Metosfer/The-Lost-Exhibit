using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneQuest : MonoBehaviour
{
    public Transform[] stonePosition; // Массив с позициями камней
    public TextMeshProUGUI text; // Текст для вывода результата

    public GameObject startImage; // Изображение начала
    public GameObject winImage; // Изображение победы

    [HideInInspector] public Transform currentStonePosition; // Текущая позиция камня
    [HideInInspector] public int index; // Индекс текущего камня

    [HideInInspector] public GameObject currentStone; // Текущий камень

    [HideInInspector] public bool isInRotationStep; // Флаг, указывающий, что идет вращение
    [HideInInspector] public bool[] checkRotation = new bool[3] { false, false, false }; // Массив для проверки вращения

    private void Start()
    {
        winImage.SetActive(false);
        currentStonePosition = stonePosition[0];
        this.GetComponent<Animator>().enabled = false;
        index = 0;
        isInRotationStep = false;
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        startImage.SetActive(true);
        yield return new WaitForSeconds(4);  // Время показа стартового изображения
        startImage.SetActive(false);  // Скрываем стартовое изображение

    }

    private void Update()
    {
        // Если показывается startImage, игнорируем клики на камни
        if (startImage.activeSelf) return;
        if (isInRotationStep) RotateStone();
        if (text.text == "LOOSE\nR FOR RESTART" && Input.GetKeyDown(KeyCode.R)) RestartGame();
    }

    public void CheckTriggers()
    {
        if (checkRotation[0] && checkRotation[1] && checkRotation[2]) // Если все камни в нужном положении
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play("WinStoneQuest");
            winImage.SetActive(true); // Победа
        }
        else // Если что-то не так
        {
            text.text = "LOOSE\nR FOR RESTART"; // Выводим сообщение о проигрыше
        }
    }


    private void RotateStone()
    {
        // Вращение по оси Yaw (основа вращения вокруг Y)
        if (Input.GetKeyDown(KeyCode.A)) // Поворот влево (по Y)
        {
            currentStone.transform.Rotate(0, 90, 0); // Поворот по оси Y (Yaw)
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Поворот вправо (по Y)
        {
            currentStone.transform.Rotate(0, -90, 0); // Поворот по оси Y (Yaw)
        }

        // Вращение по оси Pitch (вверх/вниз)
        else if (Input.GetKeyDown(KeyCode.W)) // Поворот вверх (по X)
        {
            currentStone.transform.Rotate(90, 0, 0); // Поворот по оси X (Pitch)
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Поворот вниз (по X)
        {
            currentStone.transform.Rotate(-90, 0, 0); // Поворот по оси X (Pitch)
        }

        // Вращение по оси Roll (вокруг оси Z)
        else if (Input.GetKeyDown(KeyCode.Q)) // Поворот по часовой стрелке (по Z)
        {
            currentStone.transform.Rotate(0, 0, 90); // Поворот по оси Z (Roll)
        }
        else if (Input.GetKeyDown(KeyCode.E)) // Поворот против часовой стрелки (по Z)
        {
            currentStone.transform.Rotate(0, 0, -90); // Поворот по оси Z (Roll)
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmPlacement();
            if (index == 3) CheckTriggers();
        }
    }

    private void ConfirmPlacement()
    {
        isInRotationStep = false; // Останавливаем вращение

        // Проверяем, что камень правильно расположен и правильно ориентирован
        if (currentStone.transform.eulerAngles.x > -10 && currentStone.transform.eulerAngles.x < 10 &&
            currentStone.transform.eulerAngles.y > -10 && currentStone.transform.eulerAngles.y < 10 &&
            currentStone.transform.eulerAngles.z > -10 && currentStone.transform.eulerAngles.z < 10)
        {
            checkRotation[index] = true; // Отмечаем, что вращение правильное
        }

        index++; // Переходим к следующему камню
        if (index < stonePosition.Length) currentStonePosition = stonePosition[index]; // Обновляем позицию для следующего камня
    }

    // Метод для перезапуска игры
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезагружаем текущую сцену
    }
}
