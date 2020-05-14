using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEnemy : Weapon
{
    public static readonly float minFireRate = 0.001f;
    
    [SerializeField] protected float fireRate; //La vitesse de tir de l'arme
    [SerializeField] protected float bulletDamage; //Les dégats de l'arme
    [SerializeField] protected float bulletSpeed; //La vitesse des tirs

    [SerializeField] protected float timeWaitBegin; //Le temps avant que l'arme puisse commencer au début

    protected bool canShoot;
    protected float timerShoot;
    protected float timerWaitBegin;

    public void Awake() {
        fireRate = fireRate <= minFireRate ? minFireRate : fireRate;
        canShoot = true;
        timerShoot = 0;
        timerWaitBegin = 0;
    }

    protected void reloadShoot() {
        timerShoot -= Mathf.Floor(timerShoot / fireRate) * fireRate;
        timerShoot = timerShoot < 0 ? 0 : timerShoot;
        canShoot = false;
    }

    public void updateTimer() {
        if (timerWaitBegin < timeWaitBegin) timerWaitBegin += Time.fixedDeltaTime;
        else if (timerShoot <= fireRate) timerShoot += Time.fixedDeltaTime;
        if (timerShoot >= fireRate) canShoot = true;
    }
}
