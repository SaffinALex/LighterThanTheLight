using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();

        if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
            {
                if (App.playerManager.swapWeapons(this.getItemIndex(), sender.getItemIndex()))
                {
                    Debug.Log("Weapon Equiped !");
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                }
            }
        }else if (dragObj is ShopObject)
        {
            if (App.playerManager.BuyWeapon(((dragObj as ShopObject).shopSlot.getItem() as ItemWeapon),this.getItemIndex()))
            {
                Debug.Log("Weapon bought !");
                EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
                EquipmentManager.GetEquipmentUI().reloadShopPanel();
            }
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<WeaponPlayer>();
    }
}
