using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float posx = transform.position.x;
        float posy = transform.position.y;
        GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(0f, -1f, 0f) * speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EntitySpaceShipBehavior>().getDamage(damage);
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EntitySpaceShipBehavior>().getDamage(damage);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public int getDamage()
    {
        return damage;
    }
}
