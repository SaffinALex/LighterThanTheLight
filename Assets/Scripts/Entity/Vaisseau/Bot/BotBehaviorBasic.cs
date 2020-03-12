using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorBasic : EntitySpaceShipBehavior
{
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = Random.Range(0, 2);
        TimeMove = 3.0f;
        //r2d.velocity = transform.forward * speed;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        move();
        shoot();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    override
    public void move()
    {
        /*
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
        }*/
        /*
        if(IsAtRight)
        {
            animator.SetBool("isAtRight", false);
        }
        if(!IsAtRight)
        {
            animator.SetBool("isAtRight", true);
        }
        force.y -= scrolling;
        R2d.velocity = force;
        */
        /*
        if (animator.GetBool("isAtRight"))
        {
            animator.SetBool("isAtRight", false);

        }
        else
        {
            animator.SetBool("isAtRight", true);
        }*/
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
