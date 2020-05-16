using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeWeaponSlot : InventorySlot
{
    private int weaponIndex;

    public int WeaponIndex { get => weaponIndex; set => weaponIndex = value; }
    protected override void actionOnDrop(PointerEventData eventData)
    {
        Debug.Log("Weapon Upgrade Equiped !");
        DraggableObject dragObj = eventData.pointerDrag.GetComponent<DraggableObject>();

        if (dragObj is InventoryObject)
        {
            InventorySlot sender = dragObj.getInventorySlotParent();
            if (sender != null)
                if (App.playerManager.swapWeaponUpgrades(weaponIndex, this.getItemIndex(), sender.getItemIndex()))
                    EquipmentManager.GetEquipmentUI().reloadInventoryPanel();
        }
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        DraggableObject upgradeObj = eventData.pointerDrag.GetComponent<DraggableObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeWeapon>();
    }
}
