using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Le but de cette classe est de générer une récompense lors de la destruction d'une entité lors du jeu
 * Par récompense on sous-entend un sous-ensemble de l'ensemble { Score, Argent, Amélioration, Arme }
 */
public abstract class DropReward : MonoBehaviour {

    //Lorsque le gameObject associé meurt on appelle une récompense
    void OnDestroy(){
        DropAReward();
    }

    protected abstract void DropAReward();
}
