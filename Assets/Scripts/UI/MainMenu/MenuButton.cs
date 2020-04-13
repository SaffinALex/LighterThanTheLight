using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent (typeof(TextMeshProUGUI))]
public class MenuButton : MonoBehaviour
{
    public FontStyles baseFontStyle;

    private void Start() {
        baseFontStyle = GetComponentInChildren<TextMeshProUGUI>().fontStyle;
    }

    public void mouseEnter(){
        GetComponentInChildren<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;;
    }

    public void mouseExit(){
        GetComponentInChildren<TextMeshProUGUI>().fontStyle = baseFontStyle;
    }

    void OnDisable()
    {
        GetComponentInChildren<TextMeshProUGUI>().fontStyle = baseFontStyle;
    }
}