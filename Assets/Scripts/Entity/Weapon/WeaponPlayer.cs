using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPlayer : MonoBehaviour
{
    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public int size;
    private bool canShoot = true;
    public float fireRateBase;
    private float fireRate;
    public GameObject bullet;
    public float bulletSpeedBase;
    private float bulletSpeed;
    private float damage;
    // Start is called before the first frame update
    public void Initialize(){
        setBulletSpeed(bulletSpeedBase);
        setFireRate(fireRateBase);
        damage = bullet.GetComponent<Bullet>().damage;
        for(int i=0; i<upgradeWeapons.Count; i++){
            upgradeWeapons[i].StartUpgrade(this);
        }
       
    }
    abstract public void shoot(Transform t);
    public void setBulletSpeed(float s){
        bulletSpeed = s;
    }
    public float getBulletSpeed(){
        return bulletSpeed;
    }
    public float getDamage(){
        return damage;
    }
    public void setDamage(float s){
        damage = s;
    }
    public bool getCanShoot(){
        return canShoot;
    }
    public float getFireRate(){
        return fireRate;
    }
    public void setFireRate(float s){
        fireRate = s;
    }

    private IEnumerator Shoot(){
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
