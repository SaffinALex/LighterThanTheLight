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
    private float p3;
    private bool right;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        p1 = transform.position.x + EnnemiesBorder.size.x/2;
        p2 = transform.position.x;
        p3 = transform.position.x - EnnemiesBorder.size.x / 2;
        positionX = transform.position.x;
        positionY = transform.position.y;
        type = "BotBasic";

        if (GetComponentInParent<BotBehaviorBasic>().transform.position.x < 0)
            right = true;
        else
            right = false;
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
        if (right) { 
            if (transform.position.x > p1 - 1)
            {
                positionX = -EnnemiesBorder.size.x / 2;
            }
            else if (transform.position.x < p2 + 1)
            {
                positionX = EnnemiesBorder.size.x / 2;
            }
        }
        else
        {
            if (transform.position.x < p3 + 1)
            {
                positionX = EnnemiesBorder.size.x / 2;
            }
            else if (transform.position.x > p2 - 1)
            {
                positionX = -EnnemiesBorder.size.x / 2;
            }
        }
        
        positionY = transform.position.y - scrolling;
    }

    override
    public void move()
    {
        Vector3 direction = (new Vector3(positionX, positionY, transform.position.z) - transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        r2d.velocity = force;
    }

    override
    public void shoot()
    {
        if (isShooting)
        {
            StartCoroutine("Shoot");
            weapon.GetComponent<Weapon>().shoot(transform.Find("Shoot position"));
        }
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

    public override string getType()
    {
        type = "BotBasic";
        return type;
    }
}
