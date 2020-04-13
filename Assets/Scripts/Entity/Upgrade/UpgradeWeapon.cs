using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeWeapon : Upgrade
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//A l'obtention de l'Upgrade

    public abstract void StartUpgrade(WeaponPlayer weapon);
//Avant la suppression de l'Upgrade.

    public abstract void EndUpgrade(WeaponPlayer weapon);

}
