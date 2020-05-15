using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


/**
 * Le NodeElement permet de représenter le contenu d'un node dans l'arbre de sélection de niveaux
 * Il a un début et une fin, ce qui se passe entre deux est au libre arbitre du programmeur
 */
public abstract class NodeElement : MonoBehaviour
{
    //Evénement permettant de signaler la fin du travail d'un nodeElement
    protected UnityEvent eventEnd = new UnityEvent();

    //Le score permet de représenter la difficulté du NodeElement
    //Il aura pour objectif d'être transmis aux nodesEnfants
    protected float scoreDifficulty;

    //Getter Setter de scoreDifficulty
    public float GetScoreDifficulty(){ return scoreDifficulty; }
    public void SetScoreDifficulty(float score){ scoreDifficulty = score; }

    //Permet d'initialiser le NodeElement
    //C'est là où on pourra faire des calculs du nouveau score et générer un niveau en fonction
    public abstract void InitializeNode(float score = 0);
    
    //Fonction permettant de lancer le travail du NodeElement
    public abstract void Begin();

    //Fonction permettant de signaler la fin du NodeElement
    protected void End(){
        eventEnd.Invoke();
    }

    //Permet de récupérer l'emitteur de fin d'événement
    public UnityEvent GetEvent(){
        return eventEnd;
    }
}
