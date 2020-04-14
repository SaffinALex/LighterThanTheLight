using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    public float updateTimer = 0.5f;
    public int maxDot = 3;
    private float timer;
    private int nbDot;
    private string baseText;
    void Start()
    {
        timer = 0;
        nbDot = 0;
        baseText = GetComponent<TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if(timer>=updateTimer){
            nbDot++;
            if(nbDot > maxDot)
                nbDot = 0;
            string text = baseText+ " ";
            for(int i = 0;i< nbDot; i++)
                text+= ".";
            GetComponent<TextMeshProUGUI>().text = text;
            timer = 0;
        }
    }
}
