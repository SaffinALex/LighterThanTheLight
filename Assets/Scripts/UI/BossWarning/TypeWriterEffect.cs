using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    private string currentText = "";
    private string text;

    private void Start() {
        if(text == null || text.Length == 0)
            text = GetComponent<TextMeshProUGUI>().text;
    }

    public void resetText(){
        currentText = "";
        if(text == null || text.Length == 0)
            text = GetComponent<TextMeshProUGUI>().text;
        GetComponent<TextMeshProUGUI>().text = currentText;
    }

    public void showText(){ resetText(); StartCoroutine(ShowText()); }

    IEnumerator ShowText(){
        for(int i = 0; i <= text.Length; i++){
            currentText = text.Substring(0,i);
            GetComponent<TextMeshProUGUI>().SetText(currentText);
            yield return new WaitForSeconds(delay);
        }
        yield return null;
    }
}
