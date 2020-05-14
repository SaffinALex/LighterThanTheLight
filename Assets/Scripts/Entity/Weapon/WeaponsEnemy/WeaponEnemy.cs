using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEnemy : Weapon
{
    public static readonly float minFireRate = 0.001f;

    [Header("Weapon Attributes")]
    [SerializeField] protected float fireRate; //La vitesse de tir de l'arme
    [SerializeField] protected float bulletDamage; //Les dégats de l'arme
    [SerializeField] protected float bulletSpeed; //La vitesse des tirs

    [Header("Time Wait")]
    [SerializeField] protected float timeWaitBegin; //Le temps avant que l'arme puisse commencer au début

    [Header("Reload Informations")]
    [SerializeField] protected float timeReloadWaveShoot = 5f;

    protected bool canShoot;
    protected float timerShoot;
    protected float timerWaitBegin;
    protected float timerReloadWaveShoot = 0f;

    void Awake() {
        fireRate = fireRate <= minFireRate ? minFireRate : fireRate;
        canShoot = true;
        timerShoot = 0;
        timerWaitBegin = 0;
        timerReloadWaveShoot = timeReloadWaveShoot;
    }

    public void SetBulletSpeed(float s){ bulletSpeed = s; }
    public float GetBulletSpeed() { return bulletSpeed; }

    public void SetFireRate(float s) { fireRate = s; }
    public float GetFireRate() { return fireRate; }

    public void SetBulletDamage(float s) { bulletDamage = s; }
    public float GetBulletDamage() { return bulletDamage; }

    protected void reloadShoot() {
        timerShoot -= Mathf.Floor(timerShoot / fireRate) * fireRate;
        timerShoot = timerShoot < 0 ? 0 : timerShoot;
        canShoot = false;
    }

    protected void StartReload(){
        timerReloadWaveShoot = 0f;
    }

    public void updateTimer() {
        if (timerReloadWaveShoot < timeReloadWaveShoot) { timerReloadWaveShoot += Time.fixedDeltaTime; }
        if (timerWaitBegin < timeWaitBegin) timerWaitBegin += Time.fixedDeltaTime;
        else if (timerShoot <= fireRate) timerShoot += Time.fixedDeltaTime;
        if (timerShoot >= fireRate && timerReloadWaveShoot >= timeReloadWaveShoot) canShoot = true;
    }
}
