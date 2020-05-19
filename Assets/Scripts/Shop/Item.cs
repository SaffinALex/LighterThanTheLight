using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected float price = 0;
    protected bool available = true;

    public float GetPrice(){
        return price;
    }

    public void SetPrice(float price){
        this.price = price;
    }

    public bool IsAvailable(){
        return available;
    }

    protected void ObtainItem() {
        available = false;
    }

    public void RevertBuying()
    {
        available = true;
    }
}
