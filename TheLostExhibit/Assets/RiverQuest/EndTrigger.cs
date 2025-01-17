using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public float imageDurationEnd = 3f;
    public GameObject endImage;
    public bool questFinished = false;
    public GameObject stones;
    void Start()
    {
        
        endImage.SetActive(false);
    }


    private void OnTriggerEnter(Collider end)
    {
        if (end.tag == "Player" && questFinished==false)
        {
            stones.SetActive(false);
            endImage.SetActive(true);
           questFinished = true;
            StartCoroutine(WaitAndDestroy());
        }
    }
    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(imageDurationEnd);
        endImage.SetActive(false);

    }
}
