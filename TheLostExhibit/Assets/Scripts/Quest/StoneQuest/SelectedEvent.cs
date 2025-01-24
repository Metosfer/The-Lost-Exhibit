using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedEvent : MonoBehaviour
{
    public StoneQuest stoneQuest;
    [HideInInspector] public bool isClicked;

    private void Start()
    {
        isClicked = false;
    }

    // Обработчик клика по камню
    public void OnMouseDown()
    {
        if (stoneQuest.isInRotationStep == false && isClicked == false)
        {
            // Перемещаем камень на текущую позицию
            this.gameObject.transform.position = stoneQuest.currentStonePosition.position;
            isClicked = true; // Отмечаем, что камень был кликнут
            stoneQuest.currentStone = this.gameObject; // Устанавливаем текущий камень
            stoneQuest.isInRotationStep = true; // Включаем этап вращения
            this.gameObject.GetComponent<SelectedEvent>().enabled = false; // Отключаем кнопку
        }
    }
}

