using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShieldBarControl : MonoBehaviour
{
    public float minAlpha;
    public float maxAlpha;
    public float hideDelay = 0.5f;
    public float showDelay = 0.5f;
    public List<GameObject> shields;
    
    private float hideTimer;
    private float showTimer;
    private bool showing = false;
    private bool hiding = false;
    public Image image;

    private void Start() {
        showing = true;
        showTimer = showDelay;

        setNbShield(3);
    }

    // Update is called once per frame
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
            float showScale = minAlpha + ((maxAlpha-minAlpha)*(showTimer/showDelay)) ;
            Color tempColor = image.color;
            tempColor.a = showScale;
            image.color = tempColor;
        }
        else{
            Color tempColor = image.color;
            tempColor.a = maxAlpha;
            image.color = tempColor;
            showing = !showing;
            hiding = !hiding;
            hideTimer = hideDelay;
        }
    }

    public void hideChargeRoutine(){
        hideTimer -= Time.deltaTime;
        if(hideTimer>0){
            float hideScale = maxAlpha - ((maxAlpha-minAlpha)*(hideTimer/hideDelay)) ;
            Color tempColor = image.color;
            tempColor.a = hideScale;
            image.color = tempColor;
        }else{
            Color tempColor = image.color;
            tempColor.a = minAlpha;
            image.color = tempColor;
            showing = !showing;
            hiding = !hiding;
            showTimer = showDelay;
        }
    }

    public void setNbShield(int nbShield){
        foreach (Transform child in this.transform.Find("Shields").transform){ child.gameObject.SetActive(false); }

        for(int i = 0; i<nbShield; i++)
            shields[i].gameObject.SetActive(true);
    }
}
