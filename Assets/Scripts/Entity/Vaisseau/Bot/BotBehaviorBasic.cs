using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorBasic : EntitySpaceShipBehavior
{
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Direction = Random.Range(0, 2);
        TimeMove = 3.0f;
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
            if (Direction == 1) Direction = 0;
            else Direction = 1;
        }
        if (Direction == 0)
        {
            R2d.velocity = new Vector2(speedMove, -scrolling);
        }
        else if (Direction == 1)
        {
            R2d.velocity = new Vector2(-speedMove, -scrolling);
        }
        else
        {
            R2d.velocity = new Vector2(0, -scrolling);
        }
    }

    override
    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            Instantiate(weapon, transform.position, Quaternion.identity);
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
