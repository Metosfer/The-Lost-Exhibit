using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneQuest : MonoBehaviour
{

    public Transform[] stonePosition; 
    public Transform[] stones;
    public TextMeshProUGUI text; 

    public GameObject failPanel;

    public GameObject startImage; 
    public GameObject winImage; 

   [TextArea] public string failText;

    [HideInInspector] public Transform currentStonePosition; 
    [HideInInspector] public int index; 

    [HideInInspector] public GameObject currentStone; 

    [HideInInspector] public bool isInRotationStep; 
    [HideInInspector] public bool[] checkRotation = new bool[3] { false, false, false }; 

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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
        yield return new WaitForSeconds(2);  
        startImage.SetActive(false);  

    }

    private void Update()
    {
        if (startImage.activeSelf) return;
        if (isInRotationStep) RotateStone();
    }

    public void CheckTriggers()
    {
        if (checkRotation[0] && checkRotation[1] && checkRotation[2]
            && Vector3.Distance(stones[0].position, stonePosition[0].position) < 0.1f
            && Vector3.Distance(stones[1].position, stonePosition[1].position) < 0.1f
            && Vector3.Distance(stones[2].position, stonePosition[2].position) < 0.1f) 
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play("WinStoneQuest");
            winImage.SetActive(true);

            Time.timeScale = 0;
            SceneManager.LoadScene("Level_2");
        }
        else 
        {

            failPanel.SetActive(true);
            text.text = failText; 
        }
    }



    private void ConfirmPlacement()
    {
        isInRotationStep = false; 

        
        if (IsAngleCorrect(currentStone.transform.eulerAngles.x) &&
            IsAngleCorrect(currentStone.transform.eulerAngles.y) &&
            IsAngleCorrect(currentStone.transform.eulerAngles.z))
        {
            checkRotation[index] = true; 
        }

        index++; 
        if (index < stonePosition.Length) currentStonePosition = stonePosition[index]; 
    }

    private bool IsAngleCorrect(float angle)
    {
        
        angle = (angle + 360) % 360;

        
        return Mathf.Abs(Mathf.DeltaAngle(angle, 0)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, 180)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, 360)) < 10f ||
               Mathf.Abs(Mathf.DeltaAngle(angle, -180)) < 10f;
    }



    private void RotateStone()
    {
        
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            currentStone.transform.Rotate(0, 90, 0); 
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            currentStone.transform.Rotate(0, -90, 0); 
        }

        
        else if (Input.GetKeyDown(KeyCode.W)) 
        {
            currentStone.transform.Rotate(90, 0, 0); 
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        {
            currentStone.transform.Rotate(-90, 0, 0); 
        }

        
        else if (Input.GetKeyDown(KeyCode.Q)) 
        {
            currentStone.transform.Rotate(0, 0, 90); 
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            currentStone.transform.Rotate(0, 0, -90); 
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmPlacement();
            if (index == 3) CheckTriggers();
        }
    }


    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
