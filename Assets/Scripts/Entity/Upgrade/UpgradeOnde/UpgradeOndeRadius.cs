using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOndeRadius : UpgradeOnde
{
    public float radius;

    override
    public void StartUpgrade(Onde o)
    {
        o.IncreaseRadius(radius);
    }
}
