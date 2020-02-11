using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [Range(-200f, 200f)]
    public float speedRotation = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.Rotate(0,Time.deltaTime * speedRotation, 0);
    }
}
