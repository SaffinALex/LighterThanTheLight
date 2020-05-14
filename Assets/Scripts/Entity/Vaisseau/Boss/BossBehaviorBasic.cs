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

    private float p1;
    private float p2;
    private float p3;
    private float p4;

    public bool sideIsDead;
    private bool right;
    private bool pause;

    protected float timePause = 5f;
    protected float timerPause;
    private float timemove;
    private float timer;

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

        p1 = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
        p2 = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
        p3 = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
        p4 = GetComponentInParent<BossBehaviorBasic>().transform.position.y - EnnemiesBorder.size.x / 8;
        positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
        positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
        type = "BossBasic";
        r2d = GetComponentInParent<Rigidbody2D>();
        timer = 0f;
        timerPause = 0f;
        timemove = Vector2.Distance(new Vector2(positionX, 0), new Vector2(p1/2, 0)) / speedMove;
        pause = false;

        if (GetComponentInParent<BossBehaviorBasic>().transform.position.x < 0)
            right = true;
        else
            right = false;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (!needGoAway) move();
        else GoAwayMove();
        shoot();
        shoot();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        //moveto();

        if (sideIsDead)
        {
            if (gameObject.CompareTag("Cockpit"))
            {
                Destroy(this.gameObject);
            }
            else if (gameObject.CompareTag("RightSide"))
            {
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

    public void moveto()
    {
        if (right)
        {
            if (GetComponentInParent<BossBehaviorBasic>().transform.position.y > p4 + 1)
            {
                positionY = p4;
            }
            /*
            else if (timer >= timemove)
            {
                timerPause += Time.deltaTime;
                pause = true;
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
                if(timerPause >= timePause)
                {
                    timer = 0;
                    timerPause = 0;
                    pause = false;
                }

            }*/

            else if (!pause && GetComponentInParent<BossBehaviorBasic>().transform.position.x > p1 - 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
                // timer += Time.deltaTime;
            }
            else if (!pause && GetComponentInParent<BossBehaviorBasic>().transform.position.x < p2 + 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
                //timer += Time.deltaTime;
            }
            timer += Time.deltaTime;
            // Debug.Log(transform.parent.transform.position.x);
        }
        else
        {
            if (GetComponentInParent<BossBehaviorBasic>().transform.position.y > p4 + 1)
            {
                positionY = p4;
            }

            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x < p3 + 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x + EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
            else if (GetComponentInParent<BossBehaviorBasic>().transform.position.x > p2 - 1)
            {
                positionX = GetComponentInParent<BossBehaviorBasic>().transform.position.x - EnnemiesBorder.size.x / 2;
                positionY = GetComponentInParent<BossBehaviorBasic>().transform.position.y;
            }
        }
        /*
        switch (difficult)
        {
            case 0:
                positionY = p1;
                break;
            case 1:
                positionY = p2;
                break;
        }*/
    }

    override
    public void move()
    {
        GoByRoute();
        /*
        Vector3 direction = (new Vector3(positionX, positionY, 0) - GetComponentInParent<BossBehaviorBasic>().transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        r2d.velocity = force;*/
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
        }
        else
        {
            transform.position = routes[(routeToGo + 1) % routes.Length].routes.GetChild(0).position;
        }

        tParam += Time.deltaTime;
        tParam = tParam >= routes[routeToGo].timeTravel ? routes[routeToGo].timeTravel : tParam;

        if (tParam == routes[routeToGo].timeTravel)
        {
            routeToGo = (routeToGo + 1) % routes.Length;
            tParam = 0;
        }
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

        p1 = transform.position.x + 3;
        p2 = transform.position.x;
        p3 = transform.position.y - 0.5f;
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
