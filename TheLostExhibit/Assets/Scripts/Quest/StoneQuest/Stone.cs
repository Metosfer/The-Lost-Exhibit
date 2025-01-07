using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI.Table;

public class StoneQuest : MonoBehaviour
{
    public Transform[] stonePosition;
    public TextMeshProUGUI text; 

    [HideInInspector] public Transform currentStonePosition;
    [HideInInspector] public int index;


    [HideInInspector] public GameObject currentStone;

    [HideInInspector] public bool isInRotationStep;
    [HideInInspector] public bool[] checkRotation = new bool [3] {false, false, false};


    private void Start()
    {
        currentStonePosition = stonePosition[0];
        this.GetComponent<Animator>().enabled = false;
        index = 0;
        isInRotationStep = false;

    }

    private void Update()
    {
        if (isInRotationStep) RotateStone();
    }

    public void CheckTriggers()
    {

        if (index == 3)
        {
            if (checkRotation[0] && checkRotation[1] && checkRotation[2])
            {

                this.GetComponent<Animator>().enabled = true;
                this.GetComponent<Animator>().Play("WinStoneQuest");
                text.text = "Win";
            }
            else
            {
                text.text = "Loose";
            }
        }
    }

    private void RotateStone()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentStone.transform.Rotate(0, currentStone.transform.rotation.y + 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentStone.transform.Rotate(0, currentStone.transform.rotation.y - 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentStone.transform.Rotate(0, 0, currentStone.transform.rotation.z + 90);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentStone.transform.Rotate(0, 0, currentStone.transform.rotation.z - 90);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Ñonfirmation();
            if (index == 3) CheckTriggers();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Ñonfirmation()
    {
        isInRotationStep = false;

        if (currentStone.transform.eulerAngles.x > -10 && currentStone.transform.eulerAngles.x < 10 &&
            currentStone.transform.eulerAngles.y > -10 && currentStone.transform.eulerAngles.y < 10 &&
            currentStone.transform.eulerAngles.z > -10 && currentStone.transform.eulerAngles.z < 10)
        {
            checkRotation[index] = true;

        }
        
        index++;
        if (index < stonePosition.Length) currentStonePosition = stonePosition[index];
    }

}
