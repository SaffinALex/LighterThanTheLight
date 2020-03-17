using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPlayer : MonoBehaviour
{
    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public GameObject bullet;
    public float bulletSpeed;
    // Start is called before the first frame update
    abstract public void Initialize();
    abstract public void shoot(Transform t);
    public void setBulletSpeed(float s){
        bulletSpeed = s;
    }
    public float getBulletSpeed(){
        return bulletSpeed;
    }
}
