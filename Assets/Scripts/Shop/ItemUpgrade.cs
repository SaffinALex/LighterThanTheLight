using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Item
{
    protected Upgrade upgrade;

    public ItemUpgrade(Upgrade upgrade){
        this.upgrade = upgrade;
        this.price = upgrade.price;
    }

    public new Upgrade ObtainItem()
    {
        base.ObtainItem();
        return upgrade;
    }
}
