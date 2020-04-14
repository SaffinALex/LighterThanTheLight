using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneralUI_ButtonHighlight : MonoBehaviour{
    public Color highlightColor;
    public Color normalColor;
    private Text textUI;
    private void Start() {
        textUI = GetComponent<Text>();
        textUI.color  = normalColor;
    }
    public void HighlightIn()
    {
        textUI.color = highlightColor;
    }

    public void HighlightOut()
    {
        textUI.color  = normalColor;
    }
 }
