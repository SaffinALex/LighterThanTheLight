using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IDropHandler
{
    private bool active = false;
    private Color baseColor = new Color(255, 255, 255, 255);
    public Color inactiveColor;
    private int itemIndex = -1;
    public void OnDrop(PointerEventData eventData) {
        if (isActive() && eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableObject>() != null) {
            if (isValidDrop(eventData))
            {
                actionOnDrop(eventData);
            }
            else
            {
                eventData.pointerDrag.GetComponent<DraggableObject>().ResetPos();
            }
        }
    }

    public int getItemIndex() { return itemIndex; }
    public void setItemIndex(int itemIndex) { this.itemIndex = itemIndex; }

    public bool isActive() { return active; }
    public void SetActive(bool active) { 
        this.active = active;
        if(this.active)
            gameObject.GetComponent<Image>().color = new Color(baseColor.r, baseColor.g, baseColor.b, baseColor.a);
        else
            gameObject.GetComponent<Image>().color = new Color(inactiveColor.r, inactiveColor.g, inactiveColor.b, inactiveColor.a);
    }

    protected abstract bool isValidDrop(PointerEventData eventData);
    protected abstract void actionOnDrop(PointerEventData eventData);
}
