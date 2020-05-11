using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShootGoBack : BotBasic
{
    Vector2 initialPosition;
    Vector2 nextPosition;

    [SerializeField] protected float timePause = 5f; //Représente le nombre de secondes pendant lequel l'ennemi va s'arrêter pour pouvoir tirer
    [SerializeField] protected int shootNumber = 3; //Représente le nombre de fois que va tirer l'ennemi pendant sa pause

    protected float timeChangePosition;           //Représente le temps pour changer de position ( il sera calculé en fonction de la distance et la vitesse )
    protected int nextShoot;                      //Représente le prochain temps où l'ennemis devra tirer ( il est augmenté au fur et à mesure que l'ennemis tir )

    protected float timerPause = 0f;            //Représente le timer de la pause
    protected float timerChangePosition = 0f;   //Représente le timer pour changer de position ( va servir pour l'arrivée et le départ )

    protected bool beginPause = false; //Si la pause a commencé
    protected bool endPause = false; //Si la pause est terminé

    protected new void Start(){
        base.Start();
        initialPosition = transform.position;
        nextPosition = initialPosition + new Vector2(0, - EnnemiesBorder.size.y / 3);

        timeChangePosition = Vector2.Distance(initialPosition, nextPosition) / speed;
    }

    // Update is called once per frame
    protected override void Move() {
        // 1 - La pause n'a pas commencé on arrive
        if(!beginPause && timerChangePosition < timeChangePosition){
            timerChangePosition += Time.deltaTime;
            transform.position = Vector2.Lerp(initialPosition, nextPosition, Mathf.SmoothStep(0,1, timerChangePosition / timeChangePosition));
            if(timerChangePosition >= timeChangePosition){
                beginPause = true; //On commence la pause
                timerChangePosition = 0;
            }
        }
        else if(beginPause && !endPause && timerPause < timePause){
            timerPause += Time.deltaTime;
            if(timerPause > (timePause / (shootNumber + 1)) * (nextShoot + 1) && nextShoot < shootNumber){
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(0, Vector3.forward));
                nextShoot++;
            }
            if(timerPause >= timePause){
                endPause = true;
                Vector2 tmp = initialPosition;
                initialPosition = nextPosition;
                nextPosition = tmp;
            }
        }
        else if(beginPause && endPause && timerChangePosition < timeChangePosition){
            timerChangePosition += Time.deltaTime;
            transform.position = Vector2.Lerp(initialPosition, nextPosition, Mathf.SmoothStep(0, 1, timerChangePosition / timeChangePosition));
            if (timerChangePosition >= timeChangePosition) {
                Die();
            }
        }
    }
}
