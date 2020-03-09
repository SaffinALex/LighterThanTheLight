using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    public float angle = 90;
    public float speed = 50;
    public int damage = 5;
    float alive = 0.0f;

    Vector2 direction;

    // Start is called before the first frame update
    void Start() {
        direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        alive += Time.deltaTime;
        if(alive >= 5)
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if(col.gameObject.GetComponent<Meteor>() != null)
        {
            col.GetComponent<Meteor>().GiveDamage(damage);
            Destroy(this);
        }
    }
}
