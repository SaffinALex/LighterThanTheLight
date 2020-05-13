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
        routeToGo = 0;
        tParam = 0f;
        coroutine = true;
        type = "BotCircle";
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
    }

    override
    public void move()
    {
        if (coroutine)
        {
            StartCoroutine("GoByRoute");
        }
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
    public void initialize()
    {
        isShooting = true;
        isMoving = false;
        life = 6;
        routeToGo = 0;
        tParam = 0f;
        coroutine = true;
        transform.position = new Vector3(0, 0, 0);
        routes[0].transform.position = new Vector3(0, 0, 0);
        routes[1].transform.position = new Vector3(0, 0, 0);

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

            transform.position = new Vector3(shipPosition.x, shipPosition.y, 0);
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

    public override string getType()
    {
        type = "BotCircle";
        return type;
    }
}
