using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeOndeSlot : InventorySlot
{
    protected override void actionOnDrop(PointerEventData eventData)
    {
        Debug.Log("Onde Upgrade Equiped !");
    }

    protected override bool isValidDrop(PointerEventData eventData)
    {
        UpgradeObject upgradeObj = eventData.pointerDrag.GetComponent<UpgradeObject>();
        return upgradeObj.UpgradeIsOfType<UpgradeOnde>();
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
