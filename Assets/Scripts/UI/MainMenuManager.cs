using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour{
    private bool isOnTitle;

    public float waitDelay = 8.0f;
    public float waitTimer;

    void Start() {
        isOnTitle = true;
        waitTimer = 0.0f;
    }

    void Update() {
        if(isOnTitle)
            waitTimer += Time.deltaTime;

        if(waitTimer >= waitDelay && isOnTitle && Input.anyKey){
            isOnTitle = !isOnTitle;
            gameObject.transform.Find("TitlePanel").Find("PressAnyButtonText").gameObject.SetActive(false);
            GetComponentInChildren<TitleScreenTitleText>().toggleTitleText();
            GetComponentInChildren<TitleScreenButtons>().show();

            GameObject.Find("Ship").GetComponent<Animation>().Play();
        }
    }

    public void ShowMenu(){
        if(!isOnTitle)
            foreach(Transform child in transform)
                child.gameObject.SetActive(true);
    }
    public void HideMenu(){
        if(!isOnTitle)
            foreach(Transform child in transform)
                child.gameObject.SetActive(false);
    }
}
