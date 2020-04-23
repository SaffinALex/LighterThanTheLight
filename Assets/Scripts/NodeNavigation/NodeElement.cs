using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class NodeElement : MonoBehaviour
{
    protected UnityEvent eventEnd = new UnityEvent();
    //Lancer 
    public abstract void Begin();

    protected void End(){
        eventEnd.Invoke();
    }

    public UnityEvent GetEvent(){
        return eventEnd;
    }
}
