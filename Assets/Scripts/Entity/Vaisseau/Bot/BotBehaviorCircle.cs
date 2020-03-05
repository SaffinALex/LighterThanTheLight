﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCircle : EntitySpaceShipBehavior
{
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Direction = 0;
        TimeMove = 2.0f;

        //r2d.velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        move();
        shoot();
    }

    override
    public void move()
    {
        /*
        if (Input.GetKey("d"))
        {
            r2d.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey("q"))
        {
            r2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            r2d.velocity = new Vector2(0, 0);
        }
        */
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
            Instantiate(bullet, transform.position, Quaternion.identity);
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
