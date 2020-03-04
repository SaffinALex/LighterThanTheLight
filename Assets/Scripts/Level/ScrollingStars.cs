using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingStars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible(){

            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x , this.gameObject.transform.position.y+27, this.gameObject.transform.position.z);   

    }
}
