using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class InventorySlot : MonoBehaviour, IDropHandler
{
    private int itemIndex = -1;
    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("OnDrop : " + this.gameObject.name);
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableObject>() != null) {
            if (isValidDrop(eventData)){
                eventData.pointerDrag.transform.SetParent(this.transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

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

    protected abstract bool isValidDrop(PointerEventData eventData);
    protected abstract void actionOnDrop(PointerEventData eventData);
}
