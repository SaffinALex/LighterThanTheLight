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
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // 1 - La pause n'a pas commencé on arrive
        if (!beginPause && transform.position.y > p3)
        {
            timerChangePosition += Time.deltaTime;
            positionY = p3;

            if (timerChangePosition >= timeChangePosition)
            {
                beginPause = true; //On commence la pause
                timerChangePosition = 0;
            }
        }
        else if (beginPause && !endPause && timerPause < timePause)
        {
            timerPause += Time.deltaTime;
            transform.position = transform.position + new Vector3(0, -scrolling, 0);
            positionY = transform.position.y;
            shoot();
            if (timerPause >= timePause && timerChangePosition == 0)
            {
                endPause = true;
                timerPause = 0;
            }
            else if(timerPause >= timePause && timerChangePosition != 0)
            {
                beginPause = false;
                timerChangePosition = 0;
                timerPause = 0;
            }
        }

        else if (beginPause && endPause)
        {
            timerChangePosition += Time.deltaTime;
            switch(difficult){
                case 0:
                    positionY = p1;
                    break;
                case 1:
                    positionY = p2;
                    if (timerChangePosition >= timeChangePosition)
                    {
                        endPause = false;
                    }
                    break;
            }
        }
    }

    override
    public void move()
    {
        Vector3 direction = (new Vector3(positionX, positionY, transform.position.z) - transform.position).normalized;
        force = new Vector2(direction.x, direction.y) * speedMove;
        r2d.velocity = force;
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
