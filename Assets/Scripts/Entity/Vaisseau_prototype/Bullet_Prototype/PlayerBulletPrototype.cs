using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPrototype : Bullet
{
    protected Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd2D.velocity = transform.up.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Cockpit")
        || collision.gameObject.CompareTag("RightSide") || collision.gameObject.CompareTag("LeftSide"))
        {
            collision.gameObject.GetComponent<EntitySpaceShip>().InfligeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
