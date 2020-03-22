using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCircle : EntitySpaceShipBehavior
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 shipPosition;
    private bool coroutine;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Direction = 0;
        TimeMove = 2.0f;

        routeToGo = 0;
        tParam = 0f;
        coroutine = true;

        //r2d.velocity = transform.forward * speed;
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

        if (coroutine)
        {
            StartCoroutine("GoByRoute");
        }

        //positionY = transform.position.y - 0.5f;
    }

    override
    public void move()
    {
        
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
    }

    private IEnumerator GoByRoute()
    {
        coroutine = false;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedMove;

            Vector2 p0 = routes[routeToGo].GetChild(0).position;
            Vector2 p1 = routes[routeToGo].GetChild(1).position;
            Vector2 p2 = routes[routeToGo].GetChild(2).position;
            Vector2 p3 = routes[routeToGo].GetChild(3).position;

            shipPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            /*Vector3 direction = (new Vector3(shipPosition.x, shipPosition.y, transform.position.z) - transform.position).normalized;
            force = new Vector2(direction.x, direction.y) * speedMove;
            R2d.velocity = force;*/
            transform.position = new Vector3(shipPosition.x, shipPosition.y, -7);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutine = true;
    }

}
