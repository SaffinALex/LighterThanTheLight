using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    public float timeWait;
    public float timeDisapear;

    float timerWait = 0;
    float timerDisapear = 0f;

    void Start()
    {
        timerWait += Random.Range(-1f, 1f);
        timeDisapear += Random.Range(-1f, 1f);
        if (timerWait <= 0.2f) timerWait = 0.2f;
        if (timerDisapear <= 0.2f) timerDisapear = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timerWait += Time.deltaTime;
        if(timerWait >= timeWait)
        {
            timerDisapear += Time.deltaTime;
            GetComponent<Renderer>().material.SetFloat("_dissolve", 1 - (timerDisapear / timeDisapear) );
            if(timerDisapear >= timeDisapear) {
                Destroy(gameObject);
            }
        }
    }
}
