using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Item
{
    protected Upgrade upgrade;

    
    public Upgrade BuyItem(int money){
        if(money < this.price) return null;
        return upgrade;
    }
}
