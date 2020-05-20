using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeOndeSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        if (dragObj is ShopObject)
        {
            Debug.Log("trying to buy  weapon !");
            if (App.playerManager.BuyOndeUpgrade(((dragObj as ShopObject).shopSlot.getItem() as ItemUpgrade), this.getItemIndex()))
            {
                Debug.Log("Onde Upgrade bought !");
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
            else
            {
                Debug.Log("Onde Nt bought !");
            }
        }
        else if (dragObj is LootObject)
        {
            UpgradeOnde up = Instantiate((dragObj as LootObject).GetUpgrade().GetComponent<UpgradeOnde>().gameObject).GetComponent<UpgradeOnde>();
            if (App.playerManager.setOndeUpgrade(this.getItemIndex(), up))
            {
                App.playerManager.getInventory().removeUpgradeInventory((dragObj as LootObject).GetUpgrade().GetComponent<UpgradeOnde>());
                EquipmentManager.GetEquipmentUI().reloadLootPanel();
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
            }
        }
        else if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if (App.playerManager.swapOndeUpgrades(this.getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeOnde>();
    }
}
