using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarning : MonoBehaviour
{
    public float lifetimeDuration = 4f;
    private bool showing;
    private bool hiding;
    private bool holding;
    public float hideDelay = 0.5f;
    public float showDelay = 1f;
    private float hideTimer;
    private float showTimer;
    private float lifetimeTimer;
    private CanvasGroup group;
    private TypeWriterEffect twEffect;
    private WarningBox wrnBox;
    private GameObject descText;
    private GameObject holder;

    private void Start() {
        //Init
        group = GetComponent<CanvasGroup>();
        holder = gameObject.transform.GetChild(0).gameObject;
        descText = holder.transform.Find("DescriptionText").gameObject;
        twEffect = descText.GetComponent<TypeWriterEffect>();
        wrnBox = holder.GetComponentInChildren<WarningBox>();
        lifetimeTimer = lifetimeDuration;
        showing = false; hiding = false; holding = false;

        //Test
        //show();
    }
    public void show(){
        twEffect.resetText();
        wrnBox.resetBlinking();
        descText.SetActive(false);
        holder.SetActive(true); 
        showTimer= 0;
        showing = true;
        hiding = false;
    }

    public void hide(){
        hideTimer= 0;
        showing = false;
        hiding = true;
        holding = false;
    }

    private void Update() {
        if(lifetimeTimer<lifetimeDuration && holding)
            lifetimeTimer += Time.deltaTime;
        else if(holding)
            hide();

        if(showing)
            showRoutine();
        if(hiding)
            hideRoutine();
    }

    private void showRoutine(){
        showTimer += Time.deltaTime;
        if(showTimer<=showDelay){
            float showScale = showTimer/showDelay;
            group.alpha = showScale;
        }
        else{
            showing = false;
            holding = true;
            
            descText.SetActive(true);
            twEffect.showText();
            wrnBox.startBlinking();
            lifetimeTimer = 0;
        }
    }

    private void hideRoutine(){
        hideTimer += Time.deltaTime;
        if(hideTimer<=hideDelay){
            float hideScale = 1 - hideTimer/hideDelay;
            group.alpha = hideScale;
        }
        else{
            hiding = false;
            holder.SetActive(false); 
        }
    }
}
