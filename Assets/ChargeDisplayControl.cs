using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeDisplayControl : MonoBehaviour
{
    public float hideDelay = 0.5f;
    public float showDelay = 1f;
    
    public float hideTimer;
    public float showTimer;
    public bool showing = false;
    public bool hiding = false;
    public Image image;
    public RectTransform rectTranform;

    void Update()
    {
        if(showing)
            showChargeRoutine();
        if(hiding)
            hideChargeRoutine();
    }

    //Private
    public void showChargeRoutine(){
        showTimer -= Time.deltaTime;
        if(showTimer>0){
            float showScale = 1 - showTimer/showDelay;
            Color tempColor = image.color;
            print(showScale);
            tempColor.a = showScale;
            image.color = tempColor;
        }
        else{
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            showing = !showing;
        }
    }

    public void hideChargeRoutine(){
        hideTimer -= Time.deltaTime;
        if(hideTimer>0){
            float hideScale = (hideTimer/hideDelay);
            Color tempColor = image.color;
            tempColor.a = hideScale;
            image.color = tempColor;
        }
        else{
            Color tempColor = image.color;
            tempColor.a = 0f;
            image.color = tempColor;
            hiding = !hiding;
        }
    }

    //Public
    public void showCharge(){
        showing = true;
        hiding = false;
        showTimer = showDelay;
    }
    public void hideCharge(){
        showing = false;
        hiding = true;
        hideTimer = hideDelay;
    }
}
