using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOndeDamage : UpgradeOnde
{
    public float damage;

    override
    public void StartUpgrade(Onde o)
    {
        o.IncreaseDamage(damage);
    }
}
