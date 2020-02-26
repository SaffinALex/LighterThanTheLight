using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{

    private float healthPercent = 1;
    private float currentGhostPercent = 1;

    public float ghostDelay = 1;
    public float currentGhostDelay = 0;
    public float ghostFillSpeed = 0.003f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Main Slider").GetComponent<Slider>().value = healthPercent;
        gameObject.transform.Find("Ghost Slider").GetComponent<Slider>().value = currentGhostPercent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            setHealthPercent(this.healthPercent + 0.01f);
        }
        if (Input.GetKey("down"))
        {
            setHealthPercent(this.healthPercent - 0.01f);
        }

        if(currentGhostDelay <= 0 && healthPercent < currentGhostPercent){
            currentGhostPercent -= ghostFillSpeed;
            gameObject.transform.Find("Ghost Slider").GetComponent<Slider>().value = currentGhostPercent;
        }else if (currentGhostDelay > 0)
            currentGhostDelay -= Time.deltaTime;
    }

    public void setHealthPercent(float healthPercent){
        if(this.healthPercent < healthPercent){
            gameObject.transform.Find("Ghost Slider").GetComponent<Slider>().value = healthPercent;
            currentGhostPercent = healthPercent;
        }
            
        this.healthPercent = healthPercent;
        gameObject.transform.Find("Main Slider").GetComponent<Slider>().value = this.healthPercent;
        
        currentGhostDelay = ghostDelay;
    }
}
