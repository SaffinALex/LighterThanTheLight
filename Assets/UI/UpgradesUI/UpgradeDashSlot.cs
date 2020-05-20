using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDashSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyDashUpgrade(((dragObj as ShopObject).shopSlot.getItem() as ItemUpgrade), this.getItemIndex()))
            {
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
        else if (dragObj is LootObject)
        {
            UpgradeDash up = Instantiate((dragObj as LootObject).GetUpgrade().GetComponent<UpgradeDash>().gameObject).GetComponent<UpgradeDash>();
            if (App.playerManager.setDashUpgrade(this.getItemIndex(), up))
            {
                App.playerManager.getInventory().removeUpgradeInventory((dragObj as LootObject).GetUpgrade().GetComponent<UpgradeDash>());
                EquipmentManager.GetEquipmentUI().reloadLootPanel();
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
            }
        }
        else if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if (App.playerManager.swapDashUpgrades(getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeDash>();
    }
}
