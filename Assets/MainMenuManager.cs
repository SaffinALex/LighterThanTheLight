using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void ShowMenu(){
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);
    }
    public void HideMenu(){
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
