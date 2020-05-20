using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyWeapon(((dragObj as ShopObject).shopSlot.getItem() as ItemWeapon), this.getItemIndex()))
            {
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
        else if (dragObj is LootObject)
        {
            WeaponPlayer wp = Instantiate((dragObj as LootObject).GetUpgrade().GetComponent<WeaponPlayer>().gameObject).GetComponent<WeaponPlayer>();
            if (App.playerManager.setWeapon(this.getItemIndex(), wp))
            {
                App.playerManager.getInventory().removeWeaponInventory((dragObj as LootObject).GetUpgrade().GetComponent<WeaponPlayer>());
                EquipmentManager.GetEquipmentUI().reloadLootPanel();
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
            }
        }
        else if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if (App.playerManager.swapWeapons(this.getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<WeaponPlayer>();
    }
}
