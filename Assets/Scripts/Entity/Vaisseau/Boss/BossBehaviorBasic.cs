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

    public float timeWait = 1f;
    float timerWait = 0.0f;

    public bool sideIsDead;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = Random.Range(0, 2);
        TimeMove = 3.0f;

        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        positionX = transform.position.x + 3;
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

        base.Update();
        if (transform.position.x >= p1 - 0.01)
        {
            positionX = transform.position.x - 3;
        }
        else if (transform.position.x <= p2 + 0.01)
        {
            positionX = transform.position.x + 3;
        }

        /*
        if (timerWait < timeWait)
        {
            timerWait += Time.deltaTime;
            if(timerWait >= timeWait)
            {
                Debug.Log("Time wait fini !");
            }
        }*/

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
    
    new public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (life <= 0){
                if (gameObject.CompareTag("Cockpit"))
                {
                    animator.SetBool("cockpitIsDead", true);
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
            Destroy(collision.gameObject);
        }
    }

    override
    public void getDamage(int damage)
    {
        
    }

    override
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
    }
}
