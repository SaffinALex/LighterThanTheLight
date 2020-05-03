using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDamageWeapon : UpgradeWeapon
{
    public float multiplicator;
    // Start is called before the first frame updat
       void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = icone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//A l'obtention de l'Upgrade
    override
    public void StartUpgrade(WeaponPlayer weapon){
        weapon.setDamage(weapon.getDamage()*multiplicator);
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
