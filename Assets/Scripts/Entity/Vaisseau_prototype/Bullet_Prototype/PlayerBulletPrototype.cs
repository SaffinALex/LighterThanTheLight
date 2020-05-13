using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPrototype : Bullet {

    float maxTimeAlive = 4f;

    void Start()
    {
        App.sfx.PlayEffect("LaserShot", 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up.normalized);
        
        if (hit.collider != null) {
            Collider2D collision = hit.collider;
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Cockpit")
                || collision.gameObject.CompareTag("RightSide") || collision.gameObject.CompareTag("LeftSide"))
            {
                collision.gameObject.GetComponent<EntitySpaceShipBehavior>().getDamage(damage);
                Destroy(this.gameObject);
            }
        }
        maxTimeAlive -= Time.fixedDeltaTime;
        if(maxTimeAlive <= 0) Destroy(this.gameObject);
        transform.position += transform.up.normalized * speed * Time.fixedDeltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
