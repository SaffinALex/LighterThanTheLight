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

    private float default_lifetimeDuration;
    public float default_hideDelay;
    public float default_showDelay;

    private void Start() {
        //Init
        group = GetComponent<CanvasGroup>();
        holder = gameObject.transform.GetChild(0).gameObject;
        descText = holder.transform.Find("DescriptionText").gameObject;
        twEffect = descText.GetComponent<TypeWriterEffect>();
        wrnBox = holder.GetComponentInChildren<WarningBox>();
        lifetimeTimer = lifetimeDuration;
        showing = false; hiding = false; holding = false;
        default_lifetimeDuration = lifetimeDuration;
        default_showDelay = showDelay;
        default_hideDelay = hideDelay;
        //Test
        //show();
    }
    public void show(string text, float lifetimeDuration = -1, float showDelay = -1, float hideDelay = -1){
        if(lifetimeDuration != -1)
            this.lifetimeDuration = lifetimeDuration;
        else
            this.lifetimeDuration = default_lifetimeDuration;

        if(showDelay != -1)
            this.showDelay = showDelay;
        else
            this.showDelay = default_showDelay;

        if(hideDelay != -1)
            this.hideDelay = hideDelay;
        else
            this.hideDelay = default_hideDelay;

        twEffect.setText(text);
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
