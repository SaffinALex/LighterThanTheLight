using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpaceShipBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float life;
    public float speed;
    //weapon associé à un type de bullet

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

        //r2d.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 100)
        {
            Destroy(r2d);
        }

        move();
    }

    public void move()
    {
        if (Input.GetKey("d"))
        {
            r2d.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey("q"))
        {
            r2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            r2d.velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            life -= 100;
        }
    }
}
