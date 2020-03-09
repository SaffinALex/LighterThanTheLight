using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorBasic : EntitySpaceShipBehavior
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = Random.Range(0, 2);
        TimeMove = 3.0f;
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
            if (Direction == 1) Direction = 0;
            else Direction = 1;
        }
        if (Direction == 0)
        {
            R2d.velocity = new Vector2(speedMove, 0);
        }
        else if (Direction == 1)
        {
            R2d.velocity = new Vector2(-speedMove, 0);
        }
        else
        {
            R2d.velocity = new Vector2(0, 0);
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
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
    }
}
