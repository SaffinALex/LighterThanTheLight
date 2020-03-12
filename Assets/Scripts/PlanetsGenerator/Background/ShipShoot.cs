using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    public float speedShoot = 0.5f;
    public GameObject shoot;
    public Transform shootPosition;

    float shootTimer = 0.0f;
    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            if (shootTimer >= speedShoot) shootTimer = speedShoot;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }

        if (isShooting && shootTimer >= speedShoot)
        {
            shootTimer -= speedShoot;
            Instantiate(shoot, shootPosition.position, new Quaternion());
            if(App.IsInit()) App.sfx.PlayEffect("LaserShot");
        }
    }
}
