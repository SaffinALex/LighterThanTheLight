using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpaceShip : MonoBehaviour
{
    [SerializeField] protected float MaxLife; //Représente la vie maximale de l'entité
    protected bool isAlive; //Représente un boolean pour savoir si l'entité est en vie
    protected float life; //Représente la vie de l'entité

    //Fonction appelé lors de l'initialisation de l'entité
    protected void Start(){
        isAlive = true; // De base l'ennemi est en vie
        life = MaxLife; // La vie de l'ennemi est égale à sa vie maximale
    }

    public abstract float InfligeDamage(float damages); //Permet d'infliger des dégats à l''entité
}
