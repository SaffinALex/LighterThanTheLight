using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    //Max Speed of the SpaceShip
    public float speed;
    //Define the time to achieve the wanted direction
    public float acceleration = 0.2f;


    //Represent the timer to achieve
    float timerChangeVelocity = 0.0f;
    bool needChangeVelocity = false;
    Vector2 lastVelocity;
    Vector2 lastWantedVelocity;

    public GameObject shape;

    protected Rigidbody2D rgb2D;
    
    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //shape.transform.Rotate(0,Time.deltaTime * 500,0);
        timerChangeVelocity += Time.deltaTime;
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        Vector2 wantedVelocity = new Vector2(movementX, movementY).normalized * speed;
        Vector2 finalVelocity = wantedVelocity;

        if (wantedVelocity != lastWantedVelocity) {
            timerChangeVelocity = 0.0f;
            needChangeVelocity = true;
            lastVelocity = rgb2D.velocity;
            lastWantedVelocity = wantedVelocity;
        }

        if (needChangeVelocity) {
            timerChangeVelocity += Time.deltaTime;
            float percentage = timerChangeVelocity / acceleration;
            percentage = percentage > 1 ? 1 : percentage;
            finalVelocity = Vector2.Lerp(lastVelocity, wantedVelocity, timerChangeVelocity / acceleration);

            if(percentage == 1) needChangeVelocity = false;
        }


        rgb2D.velocity = finalVelocity;
    }
    
}
