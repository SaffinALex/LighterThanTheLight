using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    TextMeshProUGUI txt;
    Color baseColor;
    Button btn;
    bool interactableDelay;

    void Start ()
    {
    btn = gameObject.GetComponent<Button> ();
    txt = GetComponentInChildren<TextMeshProUGUI>();
    baseColor = txt.color;
    
    interactableDelay = btn.interactable;
    }

    void Update ()
    {
        if (btn.interactable != interactableDelay)
            if (btn.interactable)
                txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            else
                txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            
        interactableDelay = btn.interactable;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (btn.interactable)
            txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
        else
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (btn.interactable)
            txt.color = baseColor * btn.colors.pressedColor * btn.colors.colorMultiplier;
        else
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        if (btn.interactable)
            txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
        else
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (btn.interactable)
            txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
        else
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
    }

}