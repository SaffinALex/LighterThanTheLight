using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Projectile a instancier à chaque tir à rajouter.
    public List<UpgradeWeapon> upgradeWeapons;
    public int nbrMaxUpgrade;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
