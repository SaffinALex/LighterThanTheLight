using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShootEnemy : WeaponEnemy
{
    override
    public void shoot(Transform t)
    {
        if (canShoot)
        {
            Rigidbody2D playerRgbd = t.gameObject.GetComponent<Rigidbody2D>();
            int numberShoot = Mathf.RoundToInt(timerShoot / fireRate);
            Vector2 nextPositionPlayer = playerRgbd.velocity * Time.fixedDeltaTime;
            float i = 0;
            for (i = 0; i < numberShoot; i++)
            {
                GameObject o1 = Instantiate(bullet, new Vector3(t.position.x - nextPositionPlayer.x * (i / numberShoot), t.position.y + ((i / numberShoot) * bulletSpeed * Time.fixedDeltaTime) - nextPositionPlayer.y * (i / numberShoot), t.position.z), Quaternion.identity);
                o1.GetComponent<Bullet>().setSpeed(bulletSpeed);
                o1.GetComponent<Bullet>().setDamage(bulletDamage);
            }
            reloadShoot();
        }
    }
}
