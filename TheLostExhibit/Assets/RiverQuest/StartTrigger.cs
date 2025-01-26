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
    public GameObject cheeringNPC;
    public void Start()
    {
        cheeringNPC.SetActive(false);
        startImage.SetActive(false);
        stones.SetActive(false);
    }


    private void OnTriggerEnter(Collider start)
    {
        if (start.gameObject.CompareTag("Player") && !isQuestFinished)
        {
            cheeringNPC.SetActive(true);
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