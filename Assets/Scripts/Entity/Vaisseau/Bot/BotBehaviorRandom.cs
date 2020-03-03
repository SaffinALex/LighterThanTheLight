using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorRandom : EntitySpaceShipBehavior
{
    private Rigidbody2D r2d;
    private int direction;
    private float timeMove;

    public GameObject bullet;
    public float life;
    public float speedMove;
    public float scrolling;
    public float speedShoot;
    public Animator animator;
    public bool isDead = false;
    public bool isShooting;
    public bool isMoving;
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 10);
        timeMove = Random.Range(1.5f, 4.0f);

        //r2d.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
        }

        move();
        shoot();
    }

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
            direction = Random.Range(0, 10);
            timeMove = Random.Range(0.5f, 2.0f);
        }
        if (direction == 0)
        {
            r2d.velocity = new Vector2(speedMove, -scrolling);
        }
        else if (direction == 1)
        {
            r2d.velocity = new Vector2(0, speedMove - scrolling);
        }
        else if (direction == 2)
        {
            r2d.velocity = new Vector2(-speedMove, -scrolling);
        }
        else if (direction == 3)
        {
            r2d.velocity = new Vector2(0, -speedMove - scrolling);
        }
        else
        {
            r2d.velocity = new Vector2(0, -scrolling);
        }
    }

    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (life <= 1)
            {
                animator.SetBool("isDead", true);
            }
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = false;
        yield return new WaitForSeconds(speedShoot);
        isShooting = true;
    }

    private IEnumerator Move()
    {
        isMoving = true;
        yield return new WaitForSeconds(timeMove);
        isMoving = false;

    }


}
