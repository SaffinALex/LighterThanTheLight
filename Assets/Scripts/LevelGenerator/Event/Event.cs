using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/**
 * La classe Event représente un évènement d'un niveau
 * Il a début et une fin
 * Ce qu'il se passe entre le début et la fin est au libre choix du programmeur, qui pourra la dériver à son bon vouloir
 */
public abstract class Event : MonoBehaviour
{
    protected UnityEvent eventBegin = new UnityEvent();
    protected UnityEvent eventEnd = new UnityEvent();
    private float score = 0;

    private EnemyList enemyList = new EnemyList();
    private List<GameObject> listEnemies = new List<GameObject>();
    private float difficulty;

    public List<GameObject> ListEnemies { get => listEnemies; set => listEnemies = value; }
    public float Difficulty { get => difficulty; set => difficulty = value; }
    public EnemyList EnemyList { get => enemyList; set => enemyList = value; }
    public float Score { get => score; set => score = value; }

    private bool start = false;
    private bool end = false;
    //Si nombre négatif alors c'est un event qui se lance seulement si son prédécesseur qui a aussi un temps négatif se termine
    //Si positif il se lance après le temps positif en secondes
    //Pour un timeStart négatif on appelera un event Statique
    //Pour un timeStart positif on appelera un event Dynamique
    [SerializeField] protected float timeStart = -1;

    //Permet de récupérer l'event begin de l'event
    public UnityEvent GetEventBegin() { return eventBegin; }
    //Permet de récupérer l'event end de l'event
    public UnityEvent GetEventEnd() { return eventEnd; }

    public void RunUpdate(){
        if(this.IsRunning()){
            this.UpdateEvent();
        }
    }

    //Fonction qui sera appelé lorsque l'event débute, permet l'initialisation
    protected abstract void BeginEvent();

    //Fonction qui sera appelé lorsque l'event fonctionne
    protected abstract void UpdateEvent();

    //Fonction qui sera appelé lorsque l'event fonctionne
    public abstract float GetDifficulty();

    //Renvois vrai si l'event est dynamique
    public bool IsDynamic(){
        return timeStart >= 0;
    }

    //Renvois vrai si l'event est statique
    public bool IsStatic(){
        return !IsDynamic();
    }

    public float getTimer(){
        return timeStart;
    }

    //Permet de lancer l'event
    public void Begin(){
        if(!this.start){
            this.start = true;
            BeginEvent();
            eventBegin.Invoke();
        }
    }

    //Permet de reset l'event pour pouvoir le relancer
    public void Reset(){
        if(IsRunning()) this.End();
        this.start = false;
        this.end = false;
    }

    //Termine l'event et signale cette fin
    protected void End() {
        if(!this.end){
            this.end = true;
            eventEnd.Invoke();
        }
    }

    //Retourne vrai si l'event est terminé
    public bool IsFinished(){
        return this.end;
    }

    //Retourne vrai si l'event est en train de se dérouler
    public bool IsRunning(){
        return this.start && !this.end;
    }

    //Retourne vrai si l'event a débuté
    public bool HaveBegin(){
        return this.start;
    }
}
