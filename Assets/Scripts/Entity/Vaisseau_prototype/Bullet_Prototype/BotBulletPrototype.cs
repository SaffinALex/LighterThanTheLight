using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBulletPrototype : Bullet
{

    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, speed * Time.fixedDeltaTime);
        if (hit.collider != null)
        {
            Collider2D collision = hit.collider;
            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.GetComponent<PlayerShip>().getDamage(damage);
                Destroy(this.gameObject);
            }
        }
        transform.position += -transform.up.normalized * speed * Time.fixedDeltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}