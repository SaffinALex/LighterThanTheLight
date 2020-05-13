using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorRandom : EntitySpaceShipBehavior
{
    Vector2 initialPosition;
    Vector2 nextPosition;

    protected float timeChangePosition; //Représente le temps pour changer de position ( il sera calculé en fonction de la distance et la vitesse )
    protected float timePause = 5f;
    protected bool beginPause = false; //Si la pause a commencé
    protected bool endPause = false; //Si la pause est terminé

    protected float timerPause = 0f;            //Représente le timer de la pause
    protected float timerChangePosition = 0f;   //Représente le timer pour changer de position ( va servir pour l'arrivée et le départ )

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        type = "BotRandom";

        initialPosition = transform.position;
        nextPosition = initialPosition + new Vector2(0, -EnnemiesBorder.size.y / 3);

        timeChangePosition = Vector2.Distance(initialPosition, nextPosition) / speedMove;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        move();
    }

    override
    public void move()
    {
        // 1 - La pause n'a pas commencé on arrive
        if (!beginPause && timerChangePosition < timeChangePosition)
        {
            timerChangePosition += Time.deltaTime;
            transform.position = Vector2.Lerp(initialPosition, nextPosition, Mathf.SmoothStep(0, 1, timerChangePosition / timeChangePosition));
            if (timerChangePosition >= timeChangePosition)
            {
                beginPause = true; //On commence la pause
                timerChangePosition = 0;
            }
        }
        else if (beginPause && !endPause && timerPause < timePause)
        {
            timerPause += Time.deltaTime;
            shoot();
            if (timerPause >= timePause)
            {
                endPause = true;
                Vector2 tmp = initialPosition;
                initialPosition = nextPosition;
                nextPosition = tmp;
            }
        }
        
        else if (beginPause && endPause)
        {
            timerChangePosition += Time.deltaTime;
            transform.position = Vector2.Lerp(initialPosition, nextPosition, Mathf.SmoothStep(0, 1, timerChangePosition / timeChangePosition));

            if (timerChangePosition >= timeChangePosition)
            {
                shoot();
                //isDead = true;
            }
        }
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
        direction = Random.Range(0, 8);
    }

    public override string getType()
    {
        type = "BotRandom";
        return type;
    }
}
