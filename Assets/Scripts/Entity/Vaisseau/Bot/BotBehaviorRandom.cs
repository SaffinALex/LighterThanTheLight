using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorRandom : EntitySpaceShipBehavior
{
    private float timerWait = 0.0f;
    private float timeWait = 2.0f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = Random.Range(0, 8);

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

        if (timerWait < timeWait)
        {
            timerWait += Time.deltaTime;
        }
    }

    override
    public void move()
    {
        if (timerWait >= timeWait)
        {
            //Debug.Log("Time wait fini !");
            timerWait = 0.0f;
            Direction = Random.Range(0, 8);
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
            weapon.GetComponent<Weapon>().shoot(transform);
        }
    }

    override
    public void getDamage(int damage)
    {
        life -= damage;
    }

    override
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
        Direction = Random.Range(0, 8);
    }


}
