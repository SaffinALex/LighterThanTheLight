using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    protected WeaponPlayer weapon;

    public ItemWeapon(WeaponPlayer weapon)
    {
        this.weapon = weapon;
        this.price = weapon.price;
    }

    public new WeaponPlayer ObtainItem()
    {
        base.ObtainItem();
        return weapon;
    }
}
