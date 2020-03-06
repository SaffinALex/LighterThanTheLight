using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    public float speed;

    protected Rigidbody2D rgb2D;
    
    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        //Debug.Log(movementX);
        rgb2D.velocity = new Vector2(movementX, movementY).normalized * speed;
    }
    
}
