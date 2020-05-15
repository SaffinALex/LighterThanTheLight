using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Projectile a instancier à chaque tir à rajouter.
    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public GameObject bullet;

    public int numberUpgradeCanAdd() {
        int cpt = 0;
        for (int i = 0; i < upgradeWeapons.Count; i++)
        {
            cpt += upgradeWeapons[i].getWeight();
        }
        return nbrMaxUpgrade - cpt;
    }
    public List<UpgradeWeapon> getUpgrade(){
        return this.upgradeWeapons;
    }
    public abstract void shoot(Transform t);
}
