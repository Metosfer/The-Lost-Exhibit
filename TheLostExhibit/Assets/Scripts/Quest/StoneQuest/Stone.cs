using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneQuest : MonoBehaviour
{
    public Transform[] stonePosition; // Массив с позициями камней
    public Transform[] stones;
    public TextMeshProUGUI text; // Текст для вывода результата

    public GameObject failPanel;

    public GameObject startImage; // Изображение начала
    public GameObject winImage; // Изображение победы

   [TextArea] public string failText;

    [HideInInspector] public Transform currentStonePosition; // Текущая позиция камня
    [HideInInspector] public int index; // Индекс текущего камня

    [HideInInspector] public GameObject currentStone; // Текущий камень

    [HideInInspector] public bool isInRotationStep; // Флаг, указывающий, что идет вращение
    [HideInInspector] public bool[] checkRotation = new bool[3] { false, false, false }; // Массив для проверки вращения

    private void Start()
    {
        failPanel.SetActive(false); 
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
    }

    public void CheckTriggers()
    {
        if (checkRotation[0] && checkRotation[1] && checkRotation[2]
            && Vector3.Distance(stones[0].position, stonePosition[0].position) < 0.1f
            && Vector3.Distance(stones[1].position, stonePosition[1].position) < 0.1f
            && Vector3.Distance(stones[2].position, stonePosition[2].position) < 0.1f) // Если все камни в нужном положении
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play("WinStoneQuest");
            winImage.SetActive(true); // Победа
        }
        else // Если что-то не так
        {

            failPanel.SetActive(true);
            text.text = failText; // Выводим сообщение о проигрыше
        }
    }

    private void ConfirmPlacement()
    {
        isInRotationStep = false; // Останавливаем вращение

        // Проверяем, что камень правильно расположен и правильно ориентирован
        if (IsAngleCorrect(currentStone.transform.eulerAngles.x) &&
            IsAngleCorrect(currentStone.transform.eulerAngles.y) &&
            IsAngleCorrect(currentStone.transform.eulerAngles.z))
        {
            checkRotation[index] = true; // Отмечаем, что вращение правильное
        }

        index++; // Переходим к следующему камню
        if (index < stonePosition.Length) currentStonePosition = stonePosition[index]; // Обновляем позицию для следующего камня
    }

    private bool IsAngleCorrect(float angle)
    {
        // Приводим угол в диапазон от 0 до 360
        angle = (angle + 360) % 360;

        // Угол должен быть в пределах ±10 градусов от 0, 180, 360 или -180
        return Mathf.Abs(Mathf.DeltaAngle(angle, 0)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, 180)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, 360)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, -180)) < 10f;
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


    // Метод для перезапуска игры
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезагружаем текущую сцену
    }
}
