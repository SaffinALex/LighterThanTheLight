using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloShoot : WeaponPlayer
{
    // Start is called before the first frame update


    // Update is called once per frame
    override
    public void Initialize(){
        for(int i=0; i<upgradeWeapons.Count; i++){
            upgradeWeapons[i].StartUpgrade(this);
        }
    }
    override
    public void shoot(Transform t)
    {
        GameObject o1 = Instantiate(bullet, new Vector3(t.position.x, t.position.y, t.position.z), Quaternion.identity);
        o1.GetComponent<PlayerBullet>().setSpeed(bulletSpeed);
    }
}
