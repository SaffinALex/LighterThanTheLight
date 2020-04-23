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
}
