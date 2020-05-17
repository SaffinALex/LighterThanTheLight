using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecenterCameraPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("playerShip").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
