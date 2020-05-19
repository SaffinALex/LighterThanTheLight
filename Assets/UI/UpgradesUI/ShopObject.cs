using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopObject : DraggableObject
{
    private float price;
    public ShopSlot shopSlot;
    public override void Start()
    {
        base.Start();
        if(this.upgrade.GetComponent<Upgrade>() != null)
        {
            price = this.upgrade.GetComponent<Upgrade>().getPrice();
        }
        else if (this.upgrade.GetComponent<WeaponPlayer>() != null)
        {
            price = this.upgrade.GetComponent<WeaponPlayer>().getPrice();
        }
    }

    public float getPrice()
    {
        return price;
    }
}
