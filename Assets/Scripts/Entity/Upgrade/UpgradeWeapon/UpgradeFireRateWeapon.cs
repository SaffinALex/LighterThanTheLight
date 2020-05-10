using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireRateWeapon : UpgradeWeapon
{
    public float fireRateBonus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //A l'obtention de l'Upgrade
    override
    public void StartUpgrade(WeaponPlayer weapon){
        weapon.setFireRate(weapon.getFireRate()+fireRateBonus);
    }
    override
    public void StartUpgrade(Weapon weapon){
       // weapon.setBulletSpeed(weapon.getBulletSpeed()*multiplicator);
    }
//Avant la suppression de l'Upgrade.
    override
    public void EndUpgrade(WeaponPlayer weapon)
    {
        Destroy(this);
    }
}
