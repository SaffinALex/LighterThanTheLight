using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingWheel : MonoBehaviour
{
    public float updateTimer = 0.5f;
    private float timer;
    private float count;
    void Start()
    {
        timer = 0;
        count = 0;

        transform.Find("InnerRing").gameObject.GetComponent<Image>().fillAmount = 0;
        transform.Find("LoadingPercent").gameObject.GetComponent<TextMeshProUGUI>().text = ((int) (0 * 100)).ToString();
    }
    void Update()
    {
        //timer += Time.unscaledDeltaTime;
        if(timer>=updateTimer){
            count+=0.01f;
            if(count > 1)
                count = 0;
            updateLoadingWheel(count);
            timer = 0;
        }
    }
    public void updateLoadingWheel(float progress){
        transform.Find("InnerRing").gameObject.GetComponent<Image>().fillAmount = progress;
        transform.Find("LoadingPercent").gameObject.GetComponent<TextMeshProUGUI>().text = ((int) (progress * 100)).ToString();
    }
}