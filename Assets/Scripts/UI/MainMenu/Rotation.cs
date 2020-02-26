using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Range(-200f, 200f)]
    public float xRotationSpeed = 5f;
    [Range(-200f, 200f)]
    public float yRotationSpeed = 5f;
    [Range(-200f, 200f)]
    public float zRotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

        float xRotation = Time.deltaTime * xRotationSpeed;
        float yRotation = Time.deltaTime * yRotationSpeed;
        float zRotation = Time.deltaTime * zRotationSpeed;

        gameObject.transform.Rotate(xRotation, yRotation, zRotation);
    }
}
    