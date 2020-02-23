using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

        //r2d.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("d"))
        {
            r2d.velocity = new Vector2(200, 0);
        }
        else if (Input.GetKey("q"))
        {
            r2d.velocity = new Vector2(-200, 0);
        }
        else
        {
            r2d.velocity = new Vector2(0, 0);
        }
    }
}
