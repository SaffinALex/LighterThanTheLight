using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<UpgradeObject>() != null) {
            if (isValidDrop(eventData)){
                eventData.pointerDrag.transform.SetParent(this.transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                actionOnDrop(eventData);
            }
            else
            {
                eventData.pointerDrag.GetComponent<UpgradeObject>().ResetPos();
            }
        }
    }

    protected abstract bool isValidDrop(PointerEventData eventData);
    protected abstract void actionOnDrop(PointerEventData eventData);
}
