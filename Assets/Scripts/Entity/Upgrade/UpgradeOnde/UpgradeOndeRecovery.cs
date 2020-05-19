using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOndeRecovery : UpgradeOnde
{
    public float timeReload;

    override
    public void StartUpgrade(Onde o)
    {
        o.DecreaseTimeReload(timeReload);
    }
}
