using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLevel : NodeElement
{
    void Start()
    {
        Debug.Log("Je suis un NodeLevel");
    }
    
    public override void Begin() {
        Debug.Log("Début du niveau !");
        // this.End();
        return;
    }
}
