using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpaceShipBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    private int direction;
    private float timeMove;

    public GameObject weapon;
    public float life;
    public float speedMove;
    public float scrolling;
    public float speedShoot;
    public Animator animator;
    private bool isDead = false;
    public bool isShooting;
    public bool isMoving;
    public bool isAtRight;
    public bool isAtLeft;

    public Rigidbody2D R2d { get => r2d; set => r2d = value; }
    public int Direction { get => direction; set => direction = value; }
    public float TimeMove { get => timeMove; set => timeMove = value; }


    // Start is called before the first frame update
    protected void Start()
    {
        R2d = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (isDead)
        {
            // Destroy(this.gameObject);
            isDead = false;
            isMoving = false;

            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (life <= 1)
            {
                isDead = true;
                animator.SetBool("isDead", true);
            }
            Destroy(collision.gameObject);
        }
    }

    public abstract void move();

    public abstract void shoot();

    public abstract void initialize();

    private IEnumerator Shoot()
    {
        isShooting = false;
        yield return new WaitForSeconds(speedShoot);
        isShooting = true;
    }

    private IEnumerator Move()
    {
        isMoving = true;
        yield return new WaitForSeconds(TimeMove);
        isMoving = false;

    }


}
