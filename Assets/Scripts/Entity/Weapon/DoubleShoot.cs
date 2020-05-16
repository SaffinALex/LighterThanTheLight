using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShoot : WeaponPlayer
{
    override
    public void shoot(Transform t)
    {
        if (canShoot)
        {
            Rigidbody2D playerRgbd = GameObject.Find("playerShip").GetComponent<Rigidbody2D>();
            int numberShoot = Mathf.RoundToInt(timerShoot / fireRate);
            Vector2 nextPositionPlayer = playerRgbd.velocity * Time.fixedDeltaTime;
            float i = 0;
            for (i = 0; i < numberShoot; i++)
            {
                GameObject o1 = Instantiate(bullet, new Vector3(0.3f + t.position.x - nextPositionPlayer.x * (i / numberShoot), t.position.y + ((i / numberShoot) * getBulletSpeed() * Time.fixedDeltaTime) - nextPositionPlayer.y * (i / numberShoot), t.position.z), Quaternion.identity);
                o1.GetComponent<Bullet>().setSpeed(getBulletSpeed());
                o1.GetComponent<Bullet>().setDamage(getDamage());

                GameObject o2 = Instantiate(bullet, new Vector3(-0.3f + t.position.x - nextPositionPlayer.x * (i / numberShoot), t.position.y + ((i / numberShoot) * getBulletSpeed() * Time.fixedDeltaTime) - nextPositionPlayer.y * (i / numberShoot), t.position.z), Quaternion.identity);
                o2.GetComponent<Bullet>().setSpeed(getBulletSpeed());
                o2.GetComponent<Bullet>().setDamage(getDamage());
            }
            reloadShoot();
        }
    }
}
