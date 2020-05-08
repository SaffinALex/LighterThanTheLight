using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDashDistance : UpgradeDash
{
    public float distanceBonus;

    override
    public void StartUpgrade(Dash d){
        d.setDistance(distanceBonus);
    }
}
