using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletLaser : Bullet {

    float maxTimeAlive = 4000f;

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up.normalized, speed * Time.fixedDeltaTime);
        
        if (hits.Length > 0) {
            Collider2D collisionNear = null;
            float nearDistance = -1;
            for(int i = 0; i < hits.Length; i++){
                if (hits[i].collider.gameObject.CompareTag("Enemy") || hits[i].collider.gameObject.CompareTag("Cockpit")
                || hits[i].collider.gameObject.CompareTag("RightSide") || hits[i].collider.gameObject.CompareTag("LeftSide"))
                {
                    if(nearDistance == -1 || nearDistance > hits[i].distance){
                        collisionNear = hits[i].collider;
                        nearDistance = hits[i].distance;
                    }
                }
            }
            if(collisionNear != null){
                collisionNear.gameObject.GetComponent<EntitySpaceShipBehavior>().getDamage(damage);
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
