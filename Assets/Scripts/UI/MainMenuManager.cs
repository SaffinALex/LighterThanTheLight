using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour{
    private bool isOnTitle;

    void Start() {
        isOnTitle = true;
    }

    void Update() {
        if(isOnTitle && Input.anyKey){
            isOnTitle = !isOnTitle;
            gameObject.transform.Find("PressAnyButtonText").gameObject.SetActive(false);
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
