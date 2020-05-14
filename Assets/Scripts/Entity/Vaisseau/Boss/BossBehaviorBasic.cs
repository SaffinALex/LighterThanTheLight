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
    private float p4;

    public bool sideIsDead;
    private bool right;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        p1 = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
        p2 = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
        p3 = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
        p4 = GetComponentInParent<BossBehaviorBasic>().transform.position.y - EnnemiesBorder.size.x / 8;
        positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
        positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
        type = "BossBasic";
        r2d = GetComponentInParent<Rigidbody2D>();

        if (GetComponentInParent<BossBehaviorBasic>().transform.position.x < 0)
            right = true;
        else
            right = false;
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

        if (right)
        {
            if (GetComponentInParent<BossBehaviorBasic>().transform.position.y > p4 + 1)
            {
                positionY = p4;
            }

            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x > p1 - 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x < p2 + 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
        }
        else
        {
            if (GetComponentInParent<BossBehaviorBasic>().transform.position.y > p4 + 1)
            {
                positionY = p4;
            }

            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x < p3 + 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x > p2 - 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
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
        Vector3 direction = (new Vector3(positionX, positionY, 0) - GetComponentInParent<BossBehaviorBasic>().transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        r2d.velocity = force;
    }
    
    public new void OnCollisionEnter2D(Collision2D collision)
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

    public override string getType()
    {
        type = "BossBasic";
        return type;
    }
}
