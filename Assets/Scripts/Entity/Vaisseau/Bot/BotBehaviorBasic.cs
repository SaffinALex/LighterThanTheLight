using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorBasic : EntitySpaceShipBehavior
{
    public bool isAtRight;
    private float positionX;
    private float positionY;
    private float p1;
    private float p2;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        positionX = transform.position.x + 3;
        positionY = transform.position.y;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        move();
        shoot();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (transform.position.x >= p1 - 0.01){
            positionX = transform.position.x - 3;
        }
        else if(transform.position.x <= p2 + 0.01)
        {
            positionX = transform.position.x + 3;
        }
        
        positionY = transform.position.y - 0.05f;
    }

    override
    public void move()
    {
        Vector3 direction = (new Vector3(positionX, positionY, transform.position.z) - transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        R2d.velocity = force;
    }

    override
    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.gameObject.GetComponent<Weapon>().shoot(transform);
        }
    }

    override
    public void getDamage(int damage)
    {
        life -= damage;
    }

    override
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        positionX = transform.position.x + 3;
        positionY = transform.position.y;
    }
}
