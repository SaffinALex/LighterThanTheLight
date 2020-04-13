using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingStars : MonoBehaviour
{
    public GameObject quad;
    private Transform transformScrool;
    // Start is called before the first frame update
    void Start()
    {
        transformScrool = quad.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible(){

            this.gameObject.transform.position = new Vector3(transformScrool.position.x, transformScrool.position.y, this.gameObject.transform.position.z) ;
    }
}
