using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeShipSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyShipUpgrade(((dragObj as ShopObject).shopSlot.getItem() as ItemUpgrade), this.getItemIndex()))
            {
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
        else if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if (App.playerManager.swapShipUpgrades(this.getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeShip>();
    }
}
