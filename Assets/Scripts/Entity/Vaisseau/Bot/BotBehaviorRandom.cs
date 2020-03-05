using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorRandom : EntitySpaceShipBehavior
{
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Direction = Random.Range(0, 10);
        TimeMove = Random.Range(1.5f, 4.0f);

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
            Direction = Random.Range(0, 10);
            TimeMove = Random.Range(0.5f, 2.0f);
        }
        if (Direction == 0)
        {
            R2d.velocity = new Vector2(speedMove, -scrolling);
        }
        else if (Direction == 1)
        {
            R2d.velocity = new Vector2(0, speedMove - scrolling);
        }
        else if (Direction == 2)
        {
            R2d.velocity = new Vector2(-speedMove, -scrolling);
        }
        else if (Direction == 3)
        {
            R2d.velocity = new Vector2(0, -speedMove - scrolling);
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
