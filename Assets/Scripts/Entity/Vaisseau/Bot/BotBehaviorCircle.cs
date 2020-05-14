using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCircle : EntitySpaceShipBehavior
{
    [System.Serializable]
    public struct PathBot {
        public float timeTravel;
        public Transform routes;
    }

    [SerializeField]
    private PathBot[] routes;
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
        move();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (!needGoAway) move();
        else if(coroutine) GoAwayMove();
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
        RotateCircle();
        // if (coroutine)
        // {
        //     StartCoroutine("GoByRoute");
        // }
    }

    override
    public void initialize()
    {
        life = 6;
        routeToGo = 0;
        tParam = 0f;
        coroutine = true;
        transform.position = new Vector3(0, 0, 0);
        for(int i = 0; i < routes.Length; i++){
            if(routes[i].routes != null) routes[i].routes.position = new Vector3(0, 0, 0);
        }
    }

    protected void RotateCircle(){

        if(routes[routeToGo].routes != null){
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
        }else{
            transform.position = routes[(routeToGo + 1) % routes.Length].routes.GetChild(0).position;
        }

        tParam += Time.deltaTime;
        tParam = tParam >= routes[routeToGo].timeTravel ? routes[routeToGo].timeTravel : tParam;

        if(tParam == routes[routeToGo].timeTravel){
            routeToGo = (routeToGo + 1) % routes.Length;
            tParam = 0;
        }
    }

    // private IEnumerator GoByRoute()
    // {
    //     coroutine = false;

    //     while (tParam < 1)
    //     {
    //         tParam += Time.deltaTime * speedMove;

    //         Vector2 p0 = routes[routeToGo].GetChild(0).position;
    //         Vector2 p1 = routes[routeToGo].GetChild(1).position;
    //         Vector2 p2 = routes[routeToGo].GetChild(2).position;
    //         Vector2 p3 = routes[routeToGo].GetChild(3).position;

    //         shipPosition = Mathf.Pow(1 - tParam, 3) * p0 +
    //             3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
    //             3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
    //             Mathf.Pow(tParam, 3) * p3;

    //         transform.position = new Vector3(shipPosition.x, shipPosition.y, 0);
    //         yield return new WaitForEndOfFrame();
    //     }

    //     tParam = 0f;
    //     routeToGo += 1;

    //     if (routeToGo > routes.Length - 1)
    //     {
    //         routeToGo = 0;
    //     }

    //     coroutine = true;
    // }

    public override string getType()
    {
        type = "BotCircle";
        return type;
    }
}
