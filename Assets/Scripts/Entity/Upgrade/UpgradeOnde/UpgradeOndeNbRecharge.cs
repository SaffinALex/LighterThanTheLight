using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOndeNbRecharge : UpgradeOnde
{
    public int nbRecharge;

    override
    public void StartUpgrade(Onde o)
    {
        o.IncreaseNbRecharge(nbRecharge);
    }
}
