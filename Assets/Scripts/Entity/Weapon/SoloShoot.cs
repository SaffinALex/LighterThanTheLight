using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloShoot : WeaponPlayer
{
    // Start is called before the first frame update


    // Update is called once per frame

    override
    public void shoot(Transform t)
    {
        if(getCanShoot()){
            setCanShoot(false);
            GameObject o1 = Instantiate(bullet, new Vector3(t.position.x, t.position.y, t.position.z), Quaternion.identity);
            o1.GetComponent<Bullet>().setSpeed(getBulletSpeed());
            o1.GetComponent<Bullet>().setDamage(getDamage());
        }
    }
}
