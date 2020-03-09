using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCircle : EntitySpaceShipBehavior
{
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = 0;
        TimeMove = 2.0f;

        //r2d.velocity = transform.forward * speed;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        move();
        shoot();
    }

    override
    public void move()
    {
        if (!isMoving)
        {
            StartCoroutine("Move");
            if (Direction == 1) Direction = 2;
            else if (Direction == 2) Direction = 3;
            else if (Direction == 3) Direction = 0;
            else Direction = 1;
        }
        if (Direction == 0)
        {
            R2d.velocity = new Vector2(-speedMove, -speedMove - scrolling);
        }
        else if (Direction == 1)
        {
            R2d.velocity = new Vector2(speedMove, -speedMove - scrolling);
        }
        else if (Direction == 2)
        {
            R2d.velocity = new Vector2(speedMove, speedMove - scrolling);
        }
        else
        {
            R2d.velocity = new Vector2(-speedMove, speedMove - scrolling);
        }
    }

    override
    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.gameObject.GetComponent<Weapon>().shoot(transform);
        }
    }

    override
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
    }

}
