using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baslangic : MonoBehaviour
{
    public GameObject startImage;
    private bool isQuestFinished = false;
    public float imageDuration = 3f;
    public GameObject stones;
    public GameObject questionMark;
    public void Start()
    {
        startImage.SetActive(false);
        stones.SetActive(false);
    }


    private void OnTriggerEnter(Collider start)
    {
        if (start.gameObject.CompareTag("Player") && !isQuestFinished)
        {
            questionMark.SetActive(false);
            stones.SetActive(true);
            startImage.SetActive(true);
            isQuestFinished = true;
            StartCoroutine(WaitAndGo());
        }
    }

    IEnumerator WaitAndGo()
    {
        yield return new WaitForSeconds(imageDuration);
        startImage.SetActive(false);

    }
}