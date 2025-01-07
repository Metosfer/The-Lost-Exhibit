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

    public  void OnMouseDown()
    {
        if (stoneQuest.isInRotationStep == false && isClicked == false)
        {
            this.gameObject.transform.position = stoneQuest.currentStonePosition.position;
            isClicked = true;
            stoneQuest.currentStone = this.gameObject;
            stoneQuest.isInRotationStep = true;
            this.gameObject.GetComponent<SelectedEvent>().enabled = false;
        }
    }
}
