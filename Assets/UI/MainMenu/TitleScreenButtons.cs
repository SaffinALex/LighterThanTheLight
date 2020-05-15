using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenButtons : MonoBehaviour
{

    public float apparitionDelay = 2.0f;
    private float apparitionTimer;

    private bool showing;
    // Start is called before the first frame update
    void Start()
    {
        showing = false;
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(showing && apparitionTimer<=apparitionDelay){
            apparitionTimer += Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = apparitionTimer/apparitionDelay;
        }
    }

    public void show(){
        showing = true;
        GetComponent<CanvasGroup>().alpha = 0.0f;
        apparitionTimer = 0;
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);
    }
}
