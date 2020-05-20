using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        //Action impossible
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        return false;
    }
}
