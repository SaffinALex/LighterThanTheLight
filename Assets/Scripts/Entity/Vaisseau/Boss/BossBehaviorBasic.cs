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
        if (isAtRight)
        {
            animator.SetBool("isAtRight", false);
            //isAtRight = false;
        }
        else
        {
            animator.SetBool("isAtRight", true);
            //isAtRight = true;
        }
        R2d.velocity = force;
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

    new public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (life < 1){
                if (gameObject.CompareTag("Cockpit"))
                {
                    IsDead = true;
                    animator.SetBool("cockpitIsDead", true);
                }
                else if (gameObject.CompareTag("RightSide"))
                {
                    animator.SetBool("rightSideIsDead", true);
                }
                else if (gameObject.CompareTag("LeftSide"))
                {
                    animator.SetBool("leftSideIsDead", true);
                }
            }
            Destroy(collision.gameObject);
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
