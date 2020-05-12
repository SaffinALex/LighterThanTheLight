using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBulletPrototype : Bullet
{
    protected Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd2D.velocity = - transform.up.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerShip>().getDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}