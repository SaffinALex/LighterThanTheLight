using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;

public class AnyButtonPressedAction : MonoBehaviour
{
    public GameObject textObject;
    public float TimeBeforeActivating;
    private float timer;
    private int currentTimeShown;
    private string textToShow;
    private bool activated;
    void Start(){
        activated = false;
        textToShow = textObject.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(textObject.GetComponent<TextMeshProUGUI>().text);
        timer = 0;
        currentTimeShown = (int) TimeBeforeActivating;
        textObject.GetComponent<TextMeshProUGUI>().text = currentTimeShown.ToString();
    }
    void Update() {   
        if(timer<TimeBeforeActivating){
            timer+= Time.unscaledDeltaTime;
            if(timer>=TimeBeforeActivating)
                textObject.GetComponent<TextMeshProUGUI>().text = textToShow;
            else if(currentTimeShown != ((int) Mathf.Ceil(TimeBeforeActivating - timer))){
                currentTimeShown = (int) Mathf.Ceil(TimeBeforeActivating - timer);
                textObject.GetComponent<TextMeshProUGUI>().text = currentTimeShown.ToString();
            }
        }else
            if(Input.anyKey && !activated){
                GetComponent<Button>().onClick.Invoke();
                activated = true;
            }
    }
}
