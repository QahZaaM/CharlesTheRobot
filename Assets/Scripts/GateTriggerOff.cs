using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerOff : MonoBehaviour
{

    public GameObject wall;
    public EnemiesRemainingCalculator emrScript;

    void Start()
    {
        emrScript = GameObject.Find("EnemiesRemaining").GetComponent<EnemiesRemainingCalculator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && emrScript.EnemiesRemaining == 0)
        {
            wall.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(emrScript.DarkenToRed());
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(emrScript.LightenToWhiteFromRed());
    }
}
