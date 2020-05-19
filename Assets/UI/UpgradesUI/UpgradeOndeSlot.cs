using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeOndeSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        Debug.Log("Onde Upgrade Equiped !"); 
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();

        if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if(App.playerManager.swapOndeUpgrades(this.getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
        else if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyOndeUpgrade(((dragObj as ShopObject).shopSlot.getItem() as ItemUpgrade), this.getItemIndex()))
            {
                Debug.Log("Onde Upgrade bought !");
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeOnde>();
    }
}
