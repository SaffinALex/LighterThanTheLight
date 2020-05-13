using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCross : EntitySpaceShipBehavior
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        r2d = GetComponent<Rigidbody2D>();
        type = "BotCross";
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        // transform.Rotate(0,0,Time.deltaTime * 200);
        move();
    }

    public override string getType()
    {
        type = "BotCross";
        return type;
    }

    public override void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
    }

    public override void move()
    {
        r2d.velocity = -transform.up * speedMove;
    }

    public override void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.GetComponent<Weapon>().shoot(transform.Find("Shoot position"));
        }
    }
}
