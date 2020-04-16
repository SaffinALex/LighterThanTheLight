using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorBasic : EntitySpaceShipBehavior
{
    /*  
    Vector2 position1
    Vector2 position2
    //Bezier Espace
    Vector2 controlPoint1
    Vector2 controlPoint2
    */

    public float positionX;
    public float positionY;
    public Transform t;

    private float p1;
    private float p2;
    private float p3;

    public bool sideIsDead;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        p3 = transform.position.y - 0.5f;
        positionX = transform.position.x;
        positionY = transform.position.y;
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
        Debug.Log(p3);
        Debug.Log("position");
        Debug.Log(transform.position.y);

        if(transform.position.y > p3)
        {
            positionY = p3;
        }

        else if (transform.position.x >= p1 - 0.01)
        {
            positionX = transform.position.x - 3;
        }
        else if (transform.position.x <= p2 + 0.01)
        {
            positionX = transform.position.x + 3;
        }

        if (sideIsDead)
        {
            if (gameObject.CompareTag("Cockpit"))
            {
                Destroy(this.gameObject);
            }
            else if (gameObject.CompareTag("RightSide"))
            {
                Destroy(this.gameObject);
                animator.SetBool("rightSideIsDead", false);
            }
            else if (gameObject.CompareTag("LeftSide"))
            {
                Destroy(this.gameObject);
                animator.SetBool("leftSideIsDead", false);
            }
        }
    }

    override
    public void move()
    {
        Vector3 direction = (new Vector3(positionX, positionY, transform.position.z) - transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
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
    
    public void OnCollisionEnter2D(Collision2D collision)
    {/*
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();*/
            if (life <= 0){
                if (gameObject.CompareTag("Cockpit"))
                {
                    animator.SetBool("cockpitIsDead", true);
                    Destroy(this.transform.parent.gameObject);
                }
                else if (gameObject.CompareTag("RightSide"))
                {
                    animator.SetBool("rightSideIsDead", true);
                    animator.SetBool("isNormal", true);
                }
                else if (gameObject.CompareTag("LeftSide"))
                {
                    animator.SetBool("leftSideIsDead", true);
                    animator.SetBool("isNormal", true);
                }
            }
            /*Dstroy(collision.gameObject);
        }*/
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

        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        p3 = transform.position.y - 0.5f;
        positionX = transform.position.x;
        positionY = transform.position.y;
    }
}
