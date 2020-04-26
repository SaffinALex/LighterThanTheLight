﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorFollow : EntitySpaceShipBehavior
{
    public GameObject player;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
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
    }

    override
    public void move()
    {
        Vector3 direction = (new Vector3(player.transform.position.x, player.transform.position.y, 0) - transform.position).normalized;
        force = new Vector2(direction.x, -scrolling) * speedMove;
        R2d.velocity = force;
    }

    override
    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.gameObject.GetComponent<Weapon>().shoot(transform);
        }
    }

    override
    public void getDamage(int damage)
    {
        life -= damage;
    }

    override
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
        transform.position = new Vector3(0, 0, 0);
    }
}
