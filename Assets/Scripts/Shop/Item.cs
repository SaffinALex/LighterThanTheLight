using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected float price = 0;
    protected bool available = true;

    public float getPrice(){
        return price;
    }

    public void setPrice(float price){
        this.price = price;
    }

    public void obtainItem(){
        this.available = false;
    }

    public bool isAvailable(){
        return available;
    }
}
