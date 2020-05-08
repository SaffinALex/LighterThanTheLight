using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDashSpeed : UpgradeDash
{
    public float speedBonus;

    override
    public void StartUpgrade(Dash d){
        d.setSpeed(speedBonus);
    }
}
