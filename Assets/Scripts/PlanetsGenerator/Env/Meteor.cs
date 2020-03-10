using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector2 speed;

    public int life = 50;

    void Start()
    {

    }

    void FixedUpdate()
    {
        // Position movement
        float x = transform.localPosition.x + (speed.x * Time.deltaTime);
        float y = transform.localPosition.y + ((- speed.y + BackgroundGenerator.speed) * Time.deltaTime);
        transform.localPosition = new Vector3(x, y, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void GiveDamage(int damage)
    {
        if (life <= 0) return;
        life -= damage;
        if(life <= 0)
        {
            GetComponent<MeshDestroy>().DestroyMesh();
        }
    }

    public bool isDead()
    {
        return life <= 0;
    }
}
