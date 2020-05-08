using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    // Start is called before the first frame update
    void Start(){
        GetComponent<Rigidbody2D>().velocity = (new Vector3(0f, 1f, 0f)*speed);
    }

    // Update is called once per frame
    void Update()
    {
        float posx = transform.position.x;
        float posy = transform.position.y;
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }


   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Cockpit")
        || collision.gameObject.CompareTag("RightSide") || collision.gameObject.CompareTag("LeftSide")){
            collision.gameObject.GetComponent<EntitySpaceShipBehavior>().getDamage((int)damage);
            Destroy(this.gameObject);
        }
    }
}
