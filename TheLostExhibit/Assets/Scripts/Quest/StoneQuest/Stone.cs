using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneQuest : MonoBehaviour
{
    public Transform[] stonePosition; // ������ � ��������� ������
    public TextMeshProUGUI text; // ����� ��� ������ ����������

    public GameObject startImage; // ����������� ������
    public GameObject winImage; // ����������� ������

    [HideInInspector] public Transform currentStonePosition; // ������� ������� �����
    [HideInInspector] public int index; // ������ �������� �����

    [HideInInspector] public GameObject currentStone; // ������� ������

    [HideInInspector] public bool isInRotationStep; // ����, �����������, ��� ���� ��������
    [HideInInspector] public bool[] checkRotation = new bool[3] { false, false, false }; // ������ ��� �������� ��������

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
        yield return new WaitForSeconds(4);  // ����� ������ ���������� �����������
        startImage.SetActive(false);  // �������� ��������� �����������

    }

    private void Update()
    {
        // ���� ������������ startImage, ���������� ����� �� �����
        if (startImage.activeSelf) return;
        if (isInRotationStep) RotateStone();
        if (text.text == "LOOSE\nR FOR RESTART" && Input.GetKeyDown(KeyCode.R)) RestartGame();
    }

    public void CheckTriggers()
    {
        if (checkRotation[0] && checkRotation[1] && checkRotation[2]) // ���� ��� ����� � ������ ���������
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play("WinStoneQuest");
            winImage.SetActive(true); // ������
        }
        else // ���� ���-�� �� ���
        {
            text.text = "LOOSE\nR FOR RESTART"; // ������� ��������� � ���������
        }
    }


    private void RotateStone()
    {
        // �������� �� ��� Yaw (������ �������� ������ Y)
        if (Input.GetKeyDown(KeyCode.A)) // ������� ����� (�� Y)
        {
            currentStone.transform.Rotate(0, 90, 0); // ������� �� ��� Y (Yaw)
        }
        else if (Input.GetKeyDown(KeyCode.D)) // ������� ������ (�� Y)
        {
            currentStone.transform.Rotate(0, -90, 0); // ������� �� ��� Y (Yaw)
        }

        // �������� �� ��� Pitch (�����/����)
        else if (Input.GetKeyDown(KeyCode.W)) // ������� ����� (�� X)
        {
            currentStone.transform.Rotate(90, 0, 0); // ������� �� ��� X (Pitch)
        }
        else if (Input.GetKeyDown(KeyCode.S)) // ������� ���� (�� X)
        {
            currentStone.transform.Rotate(-90, 0, 0); // ������� �� ��� X (Pitch)
        }

        // �������� �� ��� Roll (������ ��� Z)
        else if (Input.GetKeyDown(KeyCode.Q)) // ������� �� ������� ������� (�� Z)
        {
            currentStone.transform.Rotate(0, 0, 90); // ������� �� ��� Z (Roll)
        }
        else if (Input.GetKeyDown(KeyCode.E)) // ������� ������ ������� ������� (�� Z)
        {
            currentStone.transform.Rotate(0, 0, -90); // ������� �� ��� Z (Roll)
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmPlacement();
            if (index == 3) CheckTriggers();
        }
    }

    private void ConfirmPlacement()
    {
        isInRotationStep = false; // ������������� ��������

        // ���������, ��� ������ ��������� ���������� � ��������� ������������
        if (currentStone.transform.eulerAngles.x > -10 && currentStone.transform.eulerAngles.x < 10 &&
            currentStone.transform.eulerAngles.y > -10 && currentStone.transform.eulerAngles.y < 10 &&
            currentStone.transform.eulerAngles.z > -10 && currentStone.transform.eulerAngles.z < 10)
        {
            checkRotation[index] = true; // ��������, ��� �������� ����������
        }

        index++; // ��������� � ���������� �����
        if (index < stonePosition.Length) currentStonePosition = stonePosition[index]; // ��������� ������� ��� ���������� �����
    }

    // ����� ��� ����������� ����
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ������������� ������� �����
    }
}
