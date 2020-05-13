using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPlayer : MonoBehaviour
{
    public static readonly float minFireRate = 0.001f;

    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public int size;
    protected bool canShoot;
    public float fireRateBase;
    protected float fireRate;
    public GameObject bullet;
    public int price;
    public int weight;
    public float bulletSpeedBase;
    private float bulletSpeed;
    private float damage;
    // Start is called before the first frame update
    protected float timerShoot;
    public void Initialize(){
        fireRateBase = fireRateBase <= minFireRate ? minFireRate : fireRateBase;
        setBulletSpeed(bulletSpeedBase);
        setFireRate(fireRateBase);
        damage = bullet.GetComponent<Bullet>().damage;
        for(int i=0; i<upgradeWeapons.Count; i++){
            upgradeWeapons[i].StartUpgrade(this);
        }
        canShoot = true;
        timerShoot = 0;
       
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

    protected void reloadShoot(){
        timerShoot -= Mathf.Floor(timerShoot / fireRate) * fireRate;
        timerShoot = timerShoot < 0 ? 0 : timerShoot;
        canShoot = false;
    }

    public void updateTimer(){
        if(timerShoot <= fireRate){
            timerShoot += Time.deltaTime;
        }
        if (timerShoot >= fireRate) {
            canShoot = true;
        }
    }
}
