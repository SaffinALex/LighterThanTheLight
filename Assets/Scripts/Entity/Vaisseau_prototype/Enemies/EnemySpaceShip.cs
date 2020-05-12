using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpaceShip : EntitySpaceShip
{
    [SerializeField] protected float difficulty;
    
    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        gameObject.tag = "Enemy"; // On défini le tag en tant que enemy
    }

    public override float InfligeDamage(float damages){
        damages = damages > 0 ? damages : 0;
        life -= damages;
        return damages;
    }

    //Permet d'obtenir la difficulté d'une entité
    public float GetDifficulty(){
        return difficulty;
    }

    protected abstract void Move();
}
