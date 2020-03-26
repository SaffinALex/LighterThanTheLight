using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent (typeof(TextMeshProUGUI))]
public class MenuButton : MonoBehaviour
{
    public void mouseEnter(){
        GetComponentInChildren<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;;
    }

    public void mouseExit(){
        GetComponentInChildren<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
    }
}