﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpaceShipBehavior : MonoBehaviour
{
    public List<WeaponEnemy> weapons;
    public float life;
    public float speedMove;
    public float scrolling;
    public float speedShoot;
    public Animator animator;
    public bool isShooting;
    public bool isMoving;
    //public bool isAtRight;
    public Vector2 force;
    public float score;

    const float timerTouch = 0.05f;
    protected float currTimerTouch = 0;

    public Rigidbody2D r2d { get; set; }
    public int direction { get; set; }
    public bool isDead { get; set; }
    public float difficult { get; set; }
    public string type { get; set; }

    //public bool IsAtRight { get => isAtRight; set => isAtRight = value; }


    // Start is called before the first frame update
    protected void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        gameObject.transform.parent.gameObject.SetActive(true);
        isDead = false;

        for (int i = 0; i < weapons.Count; i++) {
            weapons[i] = Instantiate(weapons[i], Vector3.zero, Quaternion.identity, transform);
        }
    }

    protected void FixedUpdate()
    {
        for (int i = 0; i < weapons.Count; i++) { weapons[i].updateTimer(); }

        if (isDead)
        {
            // Destroy(this.gameObject);
            isDead = false;
            isMoving = false;

            Destroy(transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        currTimerTouch += Time.deltaTime;
        if(life <= 0 && !isDead || transform.position.y < -EnnemiesBorder.size.y/2 || transform.position.y > EnnemiesBorder.size.y / 2 || transform.position.x < -EnnemiesBorder.size.x / 2 || transform.position.x > EnnemiesBorder.size.x / 2)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
        if (currTimerTouch >= timerTouch)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (life <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }

    public float getScore()
    {
        return score;
    }

    public void getDamage(float damage)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        currTimerTouch = 0;
        life -= damage;
    }

    public abstract string getType();

    public abstract void move();

    public abstract void initialize();

    public void shoot() {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].shoot(transform);
        }
    }
}
