using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            setHealthPercent(1f);
        }
        if (Input.GetKey("down"))
        {
            setHealthPercent(this.healthPercent - Random.Range(0.01f,0.03f));
        }

        if(currentGhostDelay <= 0 && healthPercent < currentGhostPercent){
            currentGhostPercent -= ghostFillSpeed;
            gameObject.transform.Find("Ghost Slider").GetComponent<Slider>().value = currentGhostPercent;
        }else if (currentGhostDelay > 0)
            currentGhostDelay -= Time.deltaTime;
    }

    public void setHealthPercent(float healthPercent){
        if(healthPercent < 0)
            healthPercent = 0;
        if(healthPercent > 1)
            healthPercent = 1;
        if(this.healthPercent < healthPercent){
            gameObject.transform.Find("Ghost Slider").GetComponent<Slider>().value = healthPercent;
            currentGhostPercent = healthPercent;
        }
            
        this.healthPercent = healthPercent;
        gameObject.transform.Find("Main Slider").GetComponent<Slider>().value = this.healthPercent;

        gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = (this.healthPercent*100).ToString("0");
        currentGhostDelay = ghostDelay;
    }
}
