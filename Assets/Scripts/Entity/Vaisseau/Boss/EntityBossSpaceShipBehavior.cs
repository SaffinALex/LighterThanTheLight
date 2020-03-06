using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBossSpaceShipBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    private int direction;
    private float timeMove;

    public GameObject weapon;
    public float cockpitLife;
    public float rightSideLife;
    public float leftSideLife;
    public float speedMove;
    public float speedShoot;
    public Animator animator;
    private bool cockpitIsDead = false;
    private bool rightSideIsDead = false;
    private bool leftSideIsDead = false;
    public bool isShooting;
    public bool isMoving;

    public Rigidbody2D R2d { get => r2d; set => r2d = value; }
    public int Direction { get => direction; set => direction = value; }
    public float TimeMove { get => timeMove; set => timeMove = value; }

    // Start is called before the first frame update
    void Start()
    {
        R2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rightSideIsDead)
        {
            // Destroy(this.gameObject);
            rightSideIsDead = false;
            isMoving = false;

            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.GetComponent<Weapon>().shoot(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            rightSideLife -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();
            if (rightSideLife <= 1)
            {
                rightSideIsDead = true;
                animator.SetBool("rightSideIsDead", true);
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
