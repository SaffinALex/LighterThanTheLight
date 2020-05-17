using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    protected WeaponPlayer weapon;

    public WeaponPlayer ObtainItem(){
        available = false;
        return weapon;
    }

    public ItemWeapon(WeaponPlayer weapon)
    {
        this.weapon = weapon;
        this.price = weapon.price;
    }
}
