using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : WaveEvent
{
    public float wait;
    protected float currWait;

    protected override void BeginEvent()
    {
        currWait = wait;
    }

    protected override void UpdateEvent()
    {
        // Debug.Log(wait);
        currWait -= Time.deltaTime;
        if (currWait <= 0) this.End();
    }

}
