using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Item
{
    protected Upgrade upgrade;

    public ItemUpgrade(Upgrade upgrade){
        GameObject go = GameObject.Instantiate(upgrade.gameObject);
        GameObject.DontDestroyOnLoad(go);
        go.transform.position = new Vector3(10000, 10000, 10000);
        this.upgrade = go.GetComponent<Upgrade>();
        this.price = upgrade.price;
    }

    public new Upgrade ObtainItem()
    {
        base.ObtainItem();
        return upgrade;
    }
}
