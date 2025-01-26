using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerLevel2 : MonoBehaviour
{
 public DestroyQMSC destroyQMSC;

    public GameObject tutorialText;


    private void Start()
    {
        tutorialText.SetActive(false);
        destroyQMSC = FindAnyObjectByType<DestroyQMSC>();
    }

    private void Update()
    {
        if (destroyQMSC.playerCollided == true)
        {
            tutorialText.SetActive(true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.5f);
        tutorialText.SetActive(false);
        Destroy(gameObject);
    }
}
