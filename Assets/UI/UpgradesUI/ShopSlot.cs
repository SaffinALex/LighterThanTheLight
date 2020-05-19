using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : InventorySlot
{
    private ShopPanel shopPanel;
    protected override void actionOnDrop(PointerEventData eventData)
    {
        //Action impossible
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        return false;
    }

    public Item getItem()
    {
        return shopPanel.getItem(getItemIndex());
    }

    public void setShopPanel(ShopPanel shopPanel)
    {
        this.shopPanel = shopPanel;
    }
}
