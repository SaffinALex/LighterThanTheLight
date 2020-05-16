﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpaceShipBehavior : MonoBehaviour
{
    [Header("Armes")]
    public List<WeaponEnemy> weapons;
    [SerializeField] protected bool overrideDefaultWeapon = false;
    [SerializeField] protected float startWeaponFireRate;
    [SerializeField] protected float startWeaponBulletSpeed;
    [SerializeField] protected float startWeaponBulletDamage;

    [Header("Entity properties")]
    public float life;
    [SerializeField] protected bool canDieOutside = true;
    public float speedMove;
    public float scrolling;
    public Animator animator;

    //Go Away informations
    protected bool needGoAway = false;
    protected bool alreadyGoAway = false;
    protected float timeGoAway = 1f;
    protected float timerGoAway = 0f;
    protected Vector3 saveVelocity;
    
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
            //Permet de modifier les valeurs par défauts des armes
            if(overrideDefaultWeapon){
                weapons[i].SetBulletDamage(startWeaponBulletDamage);
                weapons[i].SetBulletSpeed(startWeaponBulletSpeed);
                weapons[i].SetFireRate(startWeaponFireRate);
            }
        }
    }

    protected void FixedUpdate()
    {
        for (int i = 0; i < weapons.Count; i++) { weapons[i].updateTimer(); }

        if (isDead)
        {
            // Destroy(this.gameObject);
            isDead = false;

            Destroy(transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        currTimerTouch += Time.deltaTime;
        if((life <= 0 || ((transform.position.y < -EnnemiesBorder.size.y/2 || transform.position.y > EnnemiesBorder.size.y / 2 || transform.position.x < -EnnemiesBorder.size.x / 2 || transform.position.x > EnnemiesBorder.size.x / 2) && (canDieOutside || needGoAway))) && !isDead)
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

    public void GoAway(){
        if(!alreadyGoAway){
            alreadyGoAway = true;
            canDieOutside = true;
            needGoAway = true;
            if(r2d != null) saveVelocity = r2d.velocity;
        }
    }

    public abstract string getType();

    public abstract void move();

    protected void GoAwayMove(){
        timerGoAway += Time.deltaTime;
        timerGoAway = timerGoAway > timeGoAway ? timeGoAway : timerGoAway;
        r2d.velocity = Vector3.Lerp(saveVelocity, transform.position.normalized * 5, timerGoAway / timeGoAway) + Vector3.down * scrolling;
        
        float angle = Mathf.Atan2(r2d.velocity.y, r2d.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(Quaternion.AngleAxis(angle + 90, Vector3.forward), transform.rotation, Mathf.SmoothStep(0,1, timerGoAway / timeGoAway));
    }

    public abstract void initialize();

    public void shoot() {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].shoot(transform);
        }
    }
}
