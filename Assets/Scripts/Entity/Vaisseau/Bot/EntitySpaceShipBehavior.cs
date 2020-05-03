﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpaceShipBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    private int direction;
    private bool isDead;

    public GameObject weapon;
    public float life;
    public float speedMove;
    public float scrolling;
    public float speedShoot;
    public Animator animator;
    public bool isShooting;
    public bool isMoving;
    //public bool isAtRight;
    public Vector2 force;

    public Rigidbody2D R2d { get => r2d; set => r2d = value; }
    public int Direction { get => direction; set => direction = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    //public bool IsAtRight { get => isAtRight; set => isAtRight = value; }


    // Start is called before the first frame update
    protected void Start()
    {
        R2d = GetComponent<Rigidbody2D>();
        gameObject.transform.parent.gameObject.SetActive(true);
        IsDead = false;
    }

    protected void FixedUpdate()
    {
        if (IsDead)
        {
            // Destroy(this.gameObject);
            IsDead = false;
            isMoving = false;

            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (life <= 0 && !IsDead)
        {
            IsDead = true;
            animator.SetBool("isDead", true);
        }
    }

    public abstract void getDamage(int damage);

    public abstract void move();

    public abstract void shoot();

    public abstract void initialize();

    private IEnumerator Shoot()
    {
        isShooting = false;
        yield return new WaitForSeconds(speedShoot);
        isShooting = true;
    }
}
