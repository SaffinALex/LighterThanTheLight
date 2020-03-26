using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBox : MonoBehaviour
{
    public float hideOpacity = 0.2f;
    public float showOpacity = 1f;
    public float hideDelay = 0.5f;
    public float showDelay = 1f;
    
    private float hideTimer;
    private float showTimer;
    private bool showing = false;
    private bool hiding = false;

    private CanvasGroup group;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        resetBlinking();
    }

    private CanvasGroup GetCanvasGroup(){
        if(group == null)
            group = GetComponent<CanvasGroup>();
        return group;
    }

    public void resetBlinking(){
        hiding = false; showing = false;
        GetCanvasGroup().alpha = showOpacity;
    }

    public void startBlinking(){
        hiding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(showing)
            showRoutine();
        if(hiding)
            hideRoutine();
    }
    private void showRoutine(){
        showTimer += Time.deltaTime;
        if(showTimer<=showDelay){
            float showScale = hideOpacity + ((showOpacity - hideOpacity) * showTimer/showDelay);
            GetCanvasGroup().alpha = showScale;
        }
        else{
            showing = !showing;
            hiding = !hiding;
            hideTimer = 0;
        }
    }

    private void hideRoutine(){
        hideTimer += Time.deltaTime;
        if(hideTimer<=hideDelay){
            float hideScale = showOpacity - ((showOpacity - hideOpacity) * hideTimer/hideDelay);
            GetCanvasGroup().alpha = hideScale;
        }
        else{
            showing = !showing;
            hiding = !hiding;
            showTimer = 0;
        }
    }
}