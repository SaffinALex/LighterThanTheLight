using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDashSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        Debug.Log("Dash Upgrade Equiped !");
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();

        if(dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
            {
                if (App.playerManager.swapDashUpgrades(getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
            }
        }
        else if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyDashUpgrade(((dragObj as ShopObject).shopSlot.getItem() as ItemUpgrade), this.getItemIndex()))
            {
                Debug.Log("Dash Upgrade bought !");
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeDash>();
    }
}
