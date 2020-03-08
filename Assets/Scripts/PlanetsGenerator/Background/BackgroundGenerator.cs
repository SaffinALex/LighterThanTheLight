using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public float backgroundSpeed = 2f;
    public float backgroundDepth = 50f;

    public static float speed;

    void Start()
    {
        BackgroundGenerator.speed = backgroundSpeed;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(0f, transform.position.y - backgroundSpeed * Time.deltaTime, backgroundDepth);
    }
}
