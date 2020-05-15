using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        Debug.Log("Weapon Equiped !");
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        UpgradeObject upgradeObj = eventData.pointerDrag.GetComponent<UpgradeObject>();
        return upgradeObj.UpgradeIsOfType<WeaponPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
