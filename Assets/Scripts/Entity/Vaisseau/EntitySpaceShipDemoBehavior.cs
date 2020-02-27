using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpaceShipDemoBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    private int direction = 1;
    public GameObject bullet;
    public float life;
    public float ShootTime;
    public float speed;
    public Animator animator;
    public bool isDead = false;
    public bool isMoving = false;
    public bool canShoot = true;
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

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
        if(!isMoving){
            StartCoroutine("TimeMove");
            if(direction == 1) direction = 0;
            else direction = 1;
        }
        if (direction == 0)

        {
            r2d.velocity = new Vector2(speed, 0);
        }
        else if (direction == 1)
        {
            r2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            r2d.velocity = new Vector2(0, 0);
        }
        /*if (Input.GetKey("d"))
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
        }*/
    }

    public void shoot()
    {
        if (canShoot /*&& Input.GetKey("r")*/)
        {
            StartCoroutine("Shoot");
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (life <= 100)
            {
                animator.SetBool("isDead", true);
            }
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }

    private IEnumerator TimeMove(){
        isMoving = true;
        yield return new WaitForSeconds(2.3f);
        isMoving = false;

    }
}
