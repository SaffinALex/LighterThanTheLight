using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShootEnemy : WeaponEnemy
{
    [SerializeField] protected int maxShootToReload = 5;
    int currentShoot = 0;
    override
    public void shoot(Transform t)
    {
        if (canShoot)
        {
            Rigidbody2D playerRgbd = t.gameObject.GetComponent<Rigidbody2D>();
            int numberShoot = Mathf.RoundToInt(timerShoot / fireRate);
            Vector3 shootPosition = transform.Find("Shoot position").position;
            Vector2 nextPositionPlayer = playerRgbd.velocity * Time.fixedDeltaTime;
            float i = 0;
            for (i = 0; i < numberShoot; i++)
            {
                GameObject o1 = Instantiate(bullet, new Vector3(shootPosition.x - nextPositionPlayer.x * (i / numberShoot), shootPosition.y + ((i / numberShoot) * bulletSpeed * Time.fixedDeltaTime) - nextPositionPlayer.y * (i / numberShoot), shootPosition.z), Quaternion.identity);
                o1.GetComponent<Bullet>().setSpeed(bulletSpeed);
                o1.GetComponent<Bullet>().setDamage(bulletDamage);
                currentShoot++;
                if(currentShoot >= maxShootToReload){
                    StartReload();
                    currentShoot = 0;
                }
            }
            reloadShoot();
        }
    }
}
