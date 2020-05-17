using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/**
 * La classe LevelGeneratorInfo représente la conception d'un level
 * Celui-ci est caractérisé par une liste d'events
 * Il a pour but de diriger la vie d'un level
 * Le but étant que, lorsque tous les events on eu lieu et se sont fini, alors le level est terminé
 */
public class LevelGeneratorInfo : MonoBehaviour {
    protected bool levelEnd = false;
    bool levelStart = false; //Savoir si le niveau a commencé

    public List<Event> events = new List<Event>();
    private float difficulty = 0.0f;

    int currentStaticEvent = -1;
    List<int> eventsDynamic = new List<int>();
    List<float> eventsDynamicTimer = new List<float>();

    protected UnityEvent eventEnd = new UnityEvent();

    public float Difficulty { get => difficulty; set => difficulty = value; }

    public bool DevMod = false;

    void Awake(){
        // Debug.Log("LevelGeneratorInfo ! " + events.Count);
        levelStart = false;
    }

    public void StartLevel(){
        Debug.Log("DEBUT DU NIVEAU");
        //Initialisation de l'évènement courrant à -1 pour pouvoir commencer
        currentStaticEvent = -1;
        levelEnd = false;
        levelStart = true;
        //On écoute la fin et le début de chaques events
        for (int i = 0; i < events.Count; i++)
        {
            int j = i; //Création d'une copie de i pour pouvoir récupérer l'index, en effet i existe sur tout le contexte du for
            Event e = Instantiate(events[i]);
            e.Reset();
            e.GetEventBegin().AddListener(() => { EventBegin(j); });
            e.GetEventEnd().AddListener(() => { EventFinish(j); });
            events[i] = e;
        }

        BeginGenerator();
    }

    void Update(){
        if(DevMod && !levelStart){ StartLevel(); }
        if(!levelEnd && levelStart){
            for (int i = 0; i < eventsDynamic.Count; i++)
            {
                if (events[eventsDynamic[i]].IsRunning())
                {
                    events[eventsDynamic[i]].RunUpdate();
                }
                else
                {
                    eventsDynamicTimer[i] -= Time.deltaTime;
                    if (eventsDynamicTimer[i] <= 0)
                    {
                        Debug.Log("Début de l'event " + eventsDynamic[i]);
                        events[eventsDynamic[i]].Begin();
                    }
                }
            }
            if (currentStaticEvent >= 0) events[currentStaticEvent].RunUpdate();
        }
    }

    //Permet de récupérer l'event end de l'event
    public UnityEvent GetEventEnd() { return eventEnd; }

    void EventFinish(int index){
        Debug.Log("L'event " + index + " est terminé !");
        if(events[index].IsStatic()){
            StartPartIndex(index + 1);
        }else{
            //On supprime l'event des evenements dynamique
            RemoveIndexDynamic(index);
        }
    }

    void EventBegin(int index){
        Debug.Log("L'event " + index + " commence !");
        //On check si il y a des événements qui se lance sans que celui-ci se passe après
        if (events[index].IsStatic()) {
        }
    }

    //Ajoute un event dynamique à la liste des events dynamique
    void AddIndexDynamic(int index){
        eventsDynamic.Add(index);
        eventsDynamicTimer.Add(events[index].getTimer());
        Debug.Log("Je prévois de lancer l'event " + index + " dans " + events[index].getTimer() + "s");
    }

    //Ajoute des event dynamique à la liste des events dynamique
    void AddIndexDynamic(List<int> indexes){
        for (int i = 0; i < indexes.Count; i++) {
            AddIndexDynamic(indexes[i]); //Non récursif car on appelle la méthode avec un index
        }
    }

    //Permet de retirer un event dynamique de la liste des events dynamique
    void RemoveIndexDynamic(int index){
        int positionIndex = getPositionIndexDynamic(index);
        eventsDynamic.RemoveAt(positionIndex);
        eventsDynamicTimer.RemoveAt(positionIndex);
    }

    //Permet de récupérer la position de l'index de l'event dynamique dans la liste des events dynamique
    int getPositionIndexDynamic(int index){
        for(int i = 0; i < eventsDynamic.Count; i++){
            if(eventsDynamic[i] == index) return i;
        }
        return -1;
    }

    //Début du level
    public void BeginGenerator(){
        if(currentStaticEvent == -1){
            StartPartIndex(0);
            //On ajoute les premiers event dynamique
            List<int> dynamicsEvents = GetNextDynamics(0);
            AddIndexDynamic(dynamicsEvents);
        }
    }

    //Fonction à partir d'un index donné va lancer le level statique le plus proche et ajouter à la liste des event dynamique les évènements dynamique
    protected void StartPartIndex(int index){
        currentStaticEvent = GetNextStatic(index); //On récupère le premier event statique
        List<int> dynamicsEvents = GetNextDynamics(currentStaticEvent + 1); //On récupère la liste des évènements dynamique à partir du statique trouvé
        //Si on ne trouve aucun event statique alors le niveau est considéré comme terminé
        if (currentStaticEvent == -1) {
            EndGenerator();
            return;
        }
        //On ajoute les évènements dynamique
        AddIndexDynamic(dynamicsEvents);
        
        //On lance l'évènement statique
        events[currentStaticEvent].Begin();
    }

    protected void EndGenerator(){
        if(!levelEnd){
            Debug.Log("FIN DU NIVEAU");
            levelEnd = true;
            eventEnd.Invoke();
        }
    }

    //Retourne l'index de l'event statique le plus proche de l'index en se déplaçant en avant dans la liste
    int GetNextStatic(int index){
        for(int i = index; i < events.Count; i++){
            if(events[i].IsStatic()) return i;
        }
        return -1;
    }

    //Retourn l'index des prochains event dynamiques
    List<int> GetNextDynamics(int index){
        List<int> dynamicList = new List<int>();
        for(int i = index; i < events.Count; i++){
            if (events[i].IsStatic()) return dynamicList;
            dynamicList.Add(i);
        }
        return dynamicList;
    }
}
