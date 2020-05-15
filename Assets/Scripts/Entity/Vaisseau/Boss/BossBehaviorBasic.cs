using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorBasic : EntitySpaceShipBehavior
{
    /*  
    Vector2 position1
    Vector2 position2
    //Bezier Espace
    Vector2 controlPoint1
    Vector2 controlPoint2
    */

    public float positionX;
    public float positionY;
    public Transform t;
    
    private float p;

    public bool sideIsDead;
    private float timer = 1;
    private float time;

    [System.Serializable]
    public struct PathBot
    {
        public float timeTravel;
        public Transform routes;
    }

    [SerializeField]
    private PathBot[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 shipPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        time = 0;
        p = GetComponentInParent<BossBehaviorBasic>().transform.position.y - EnnemiesBorder.size.y / 8;
        positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
        positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
        type = "BossBasic";
        r2d = GetComponentInParent<Rigidbody2D>();
        /*
        for (int i = 0; i < routes.Length; i++)
        {
            if (routes[i].routes != null)
            {
                if(i == 0)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        routes[i].routes.GetChild(j).position = new Vector3(EnnemiesBorder.size.x / 3, GetComponentInParent<BossBehaviorBasic>().transform.position.y);
                    }
                }
                else if(i == routes.Length - 1)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        routes[i].routes.GetChild(j).position = new Vector3(-EnnemiesBorder.size.x / 3, GetComponentInParent<BossBehaviorBasic>().transform.position.y);
                    }
                }
                else
                {
                    for (int j = 0; j < 2; j++)
                    {
                        routes[i].routes.GetChild(j).position = new Vector3(EnnemiesBorder.size.x / 3, GetComponentInParent<BossBehaviorBasic>().transform.position.y);
                    }
                    for (int j = 2; j < 4; j++)
                    {
                        routes[i].routes.GetChild(j).position = new Vector3(-EnnemiesBorder.size.x / 3, GetComponentInParent<BossBehaviorBasic>().transform.position.y);
                    }
                }
            }
        }*/
    }

    new void FixedUpdate()
    {
        for (int i = 0; i < weapons.Count; i++) { weapons[i].updateTimer(); }

        if (isDead)
        {
            // Destroy(this.gameObject);
            isDead = false;
            sideIsDead = true;
        }

        if (!needGoAway) move();
        else GoAwayMove();
        shoot(0);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if(difficult == 3)
        {
            time += Time.deltaTime;
            if(time >= timer)
            {
                if (weapons[0].GetFireRate() > 0.1)
                {
                    weapons[0].SetFireRate(weapons[0].GetFireRate() - 0.01f);
                    time = 0;
                }
            }
        }

        if (sideIsDead)
        {
            if (gameObject.CompareTag("Cockpit")) {
                Destroy(transform.parent.gameObject);
            }
            else if (gameObject.CompareTag("RightSide")) {
                Destroy(this.gameObject);
                animator.SetBool("rightSideIsDead", false);
            }
            else if (gameObject.CompareTag("LeftSide"))
            {
                Destroy(this.gameObject);
                animator.SetBool("leftSideIsDead", false);
            }
        }
    }

    override
    public void move()
    {
        if (GetComponentInParent<BossBehaviorBasic>().transform.position.y > p + 1)
        {
            positionY = p;
            Vector3 direction = (new Vector3(positionX, positionY, transform.position.z) - transform.position).normalized;
            force = new Vector2(direction.x, direction.y) * speedMove;
            r2d.velocity = force;

            for (int i = 0; i < routes.Length; i++)
            {
                if (routes[i].routes != null)
                {
                    routes[i].routes.position = new Vector3(0, GetComponentInParent<BossBehaviorBasic>().transform.position.y);
                    routes[i].routes.localPosition = new Vector3(0, routes[i].routes.localPosition.y);
                }
            }
        }

        else
        {
            if (routes.Length > 0) GoByRoute();
        }
    }

    protected void GoByRoute()
    {
        if (routes[routeToGo].routes != null)
        {
            Vector2 p0 = routes[routeToGo].routes.GetChild(0).position;
            Vector2 p1 = routes[routeToGo].routes.GetChild(1).position;
            Vector2 p2 = routes[routeToGo].routes.GetChild(2).position;
            Vector2 p3 = routes[routeToGo].routes.GetChild(3).position;
            float percentT = tParam / routes[routeToGo].timeTravel;
            shipPosition = Mathf.Pow(1 - percentT, 3) * p0 +
                    3 * Mathf.Pow(1 - percentT, 2) * percentT * p1 +
                    3 * (1 - percentT) * Mathf.Pow(percentT, 2) * p2 +
                    Mathf.Pow(percentT, 3) * p3;
            transform.position = new Vector3(shipPosition.x, shipPosition.y, 0);

            if(difficult == 4)
            {
                if(routeToGo == 0 || routeToGo == 5 || routeToGo == 10)
                {
                    shoot(2);
                }
            }
        }
        else
        {
            transform.position = routes[(routeToGo + 1) % routes.Length].routes.GetChild(0).position;
            shoot(1);
        }

        tParam += Time.deltaTime;
        tParam = tParam >= routes[routeToGo].timeTravel ? routes[routeToGo].timeTravel : tParam;

        if (tParam == routes[routeToGo].timeTravel)
        {
            routeToGo = (routeToGo + 1) % routes.Length;
            tParam = 0;
        }
    }

    public void shoot(int w)
    {

        weapons[w].shoot(transform);
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {/*
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life -= collision.gameObject.GetComponent<PlayerBullet>().getDamage();*/
            if (life <= 0){
                if (gameObject.CompareTag("Cockpit"))
                {
                    animator.SetBool("cockpitIsDead", true);
                    Destroy(this.transform.parent.gameObject);
                }
                else if (gameObject.CompareTag("RightSide"))
                {
                    animator.SetBool("rightSideIsDead", true);
                    animator.SetBool("isNormal", true);
                }
                else if (gameObject.CompareTag("LeftSide"))
                {
                    animator.SetBool("leftSideIsDead", true);
                    animator.SetBool("isNormal", true);
                }
            }
            /*Dstroy(collision.gameObject);
        }*/
    }

    override
    public void initialize()
    {
        life = 6;

        positionX = transform.position.x;
        positionY = transform.position.y;

        routeToGo = 0;
        tParam = 0f;
        transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < routes.Length; i++)
        {
            if (routes[i].routes != null) routes[i].routes.position = new Vector3(0, 0, 0);
        }
    }

    public override string getType()
    {
        type = "BossBasic";
        return type;
    }
}
