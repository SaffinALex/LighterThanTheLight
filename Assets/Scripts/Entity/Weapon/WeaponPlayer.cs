using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPlayer : MonoBehaviour
{
    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public int size;
    private bool canShoot;
    public float fireRateBase;
    private float fireRate;
    public GameObject bullet;
    public int price;
    public int weight;
    public float bulletSpeedBase;
    private float bulletSpeed;
    private float damage;
    // Start is called before the first frame update
    private float timer;
    public void Initialize(){
        setBulletSpeed(bulletSpeedBase);
        setFireRate(fireRateBase);
        damage = bullet.GetComponent<Bullet>().damage;
        for(int i=0; i<upgradeWeapons.Count; i++){
            upgradeWeapons[i].StartUpgrade(this);
        }
        canShoot = true;
        timer = 0;
       
    }

    public int numberUpgradeCanAdd(){
        int cpt = 0;
        for(int i = 0; i<upgradeWeapons.Count; i++){
            cpt+= upgradeWeapons[i].getWeight();
        }
        return nbrMaxUpgrade - cpt;
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

    public void setCanShoot(bool b){
        canShoot = b;
    }

    public int getWeight(){
        return weight;
    }

    public int getPrice(){
        return price;
    }
    public float getFireRate(){
        return fireRate;
    }
    public void setFireRate(float s){
        fireRate = s;
        Debug.Log(fireRate);
    }

 

    public void addUpgradeWeapon(UpgradeWeapon u){
        upgradeWeapons.Add(u);
    }

    public void updateTimer(){
        timer += Time.deltaTime;
        if(timer >= fireRate){
            timer = 0;
            canShoot = true;
        }
    }
}
