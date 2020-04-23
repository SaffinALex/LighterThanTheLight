using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float getDamage()
    {
        return damage;
    }
    public void setDamage(float s)
    {
        damage = s;
    }
    public float getSpeed()
    {
        return speed;
    }
    public void setSpeed(float s)
    {
        speed = s;
    }

    // Start is called before the first frame update
}
