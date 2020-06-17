using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    protected WeaponPlayer weapon;

    public ItemWeapon(WeaponPlayer weapon)
    {
        GameObject go = GameObject.Instantiate(weapon.gameObject);
        GameObject.DontDestroyOnLoad(go);
        go.transform.position = new Vector3(10000, 10000, 10000);
        this.weapon = go.GetComponent<WeaponPlayer>();
        this.price = weapon.price;
    }

    public new WeaponPlayer ObtainItem()
    {
        base.ObtainItem();
        return weapon;
    }
}
