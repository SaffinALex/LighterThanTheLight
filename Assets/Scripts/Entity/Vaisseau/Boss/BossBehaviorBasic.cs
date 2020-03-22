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
    public float positionX2;
    public bool isAtRight;
    public Transform t;

    public float timeWait = 2f;
    float timerWait = 0.0f;


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

        if(timerWait < timeWait)
        {
            timerWait += Time.deltaTime;
            if(timerWait >= timeWait)
            {
                Debug.Log("Time wait fini !");
            }
        }
    }

    override
    public void move()
    {
        /*
        Debug.Log(isAtRight);
        if (isAtRight)
        {
            //animator.SetBool("isAtRight", true);
            if (transform.position.x < positionX)
            {

            }
        }
        else
        {
            //animator.SetBool("isAtRight", false);
            if (transform.position.x > positionX2)
            {

            }
            
        }
        R2d.velocity = force;*/
        Vector3 direction = (t.position - transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        Debug.Log(transform.parent.gameObject);
        //R2d.velocity = force;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = force;
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
