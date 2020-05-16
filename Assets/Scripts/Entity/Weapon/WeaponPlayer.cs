using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPlayer : Weapon
{
    public static readonly float minFireRate = 0.001f;
    
    public int size;
    protected bool canShoot;
    public float fireRateBase;
    protected float fireRate;
    public int price;
    public int weight;
    public float bulletSpeedBase;
    private float bulletSpeed;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    protected float timerShoot;
    public void Initialize(){
        fireRateBase = fireRateBase <= minFireRate ? minFireRate : fireRateBase;
        setBulletSpeed(bulletSpeedBase);
        setFireRate(fireRateBase);
        for(int i=0; i<upgradeWeapons.Count; i++){
            upgradeWeapons[i].StartUpgrade(this);
        }
        canShoot = true;
        timerShoot = 0;

        List<UpgradeWeapon> bufferUpgrades = new List<UpgradeWeapon>(new UpgradeWeapon[nbrMaxUpgrade]);
        foreach (UpgradeWeapon wp in upgradeWeapons)
            bufferUpgrades.Add(wp);
        upgradeWeapons = bufferUpgrades;
    }
    
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
