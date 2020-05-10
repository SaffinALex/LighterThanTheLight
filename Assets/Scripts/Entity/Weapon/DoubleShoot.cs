using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShoot : WeaponPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    override
    public void shoot(Transform t)
    {

        if(getCanShoot()){
            setCanShoot(false);
            GameObject o1 = Instantiate(bullet, new Vector3(t.position.x-0.2f, t.position.y, t.position.z), Quaternion.identity);
            GameObject o2 = Instantiate(bullet, new Vector3(t.position.x+0.2f, t.position.y, t.position.z), Quaternion.identity);
            o2.GetComponent<Bullet>().setSpeed(getBulletSpeed());
            o1.GetComponent<Bullet>().setSpeed(getBulletSpeed());
            o1.GetComponent<Bullet>().setDamage(getDamage());
            o2.GetComponent<Bullet>().setDamage(getDamage());
        }
    }
}
