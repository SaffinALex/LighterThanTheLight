using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorRandom : EntitySpaceShipBehavior
{
    float initialPosition;
    float nextPosition;

    protected float timeChangePosition; //Représente le temps pour changer de position ( il sera calculé en fonction de la distance et la vitesse )
    protected float timePause = 5f;
    protected bool beginPause = false; //Si la pause a commencé
    protected bool endPause = false; //Si la pause est terminé

    protected float timerPause = 0f;            //Représente le timer de la pause
    protected float timerChangePosition = 0f;   //Représente le timer pour changer de position ( va servir pour l'arrivée et le départ )

    private float positionX;
    private float positionY;
    private float p1;
    private float p2;
    private float p3;

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
        type = "BotRandom";

        p1 = transform.position.y + EnnemiesBorder.size.y / 6;
        p2 = transform.position.y;
        p3 = transform.position.y - EnnemiesBorder.size.y / 6;
        positionX = transform.position.x;
        positionY = transform.position.y;

        initialPosition = transform.position.y;
        nextPosition = initialPosition - EnnemiesBorder.size.y / 6 + 1;

        timeChangePosition = Vector2.Distance(new Vector2(0, initialPosition), new Vector2(0, nextPosition)) / speedMove;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (!needGoAway) move();
        else GoAwayMove();

        if (difficult == 2 || difficult == 3 || difficult == 4)
        {
            shoot();
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    override
    public void move()
    {
        if (routes.Length > 0) GoByRoute();

        for (int i = 0; i < routes.Length; i++)
        {
            if (routes[i].routes != null) {
                routes[i].routes.position = routes[i].routes.position + new Vector3(0, -scrolling, 0) * Time.deltaTime;
            }
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
        }
        else
        {
            transform.position = routes[(routeToGo + 1) % routes.Length].routes.GetChild(0).position;
            if(difficult == 0 || difficult == 1)
            {
                shoot();
            }
        }

        tParam += Time.deltaTime;
        tParam = tParam >= routes[routeToGo].timeTravel ? routes[routeToGo].timeTravel : tParam;

        if (tParam == routes[routeToGo].timeTravel)
        {
            routeToGo = (routeToGo + 1) % routes.Length;
            tParam = 0;
        }
    }

    override
    public void initialize()
    {
        life = 6;
        direction = Random.Range(0, 8);
    }

    public override string getType()
    {
        type = "BotRandom";
        return type;
    }
}
