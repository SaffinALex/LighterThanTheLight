using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBullet : Bullet
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
        transform.position = transform.position + new Vector3(0f, -1f, 0f) * speed * Time.deltaTime;
    }

    new private void OnTriggerEnter2D(Collider2D collision)
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

    public int getDamage()
    {
        return damage;
    }
}
