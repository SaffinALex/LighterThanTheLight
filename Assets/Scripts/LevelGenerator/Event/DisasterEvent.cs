using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterEvent : Event {

    protected float wait;

    public override float GetDifficulty(){
        return 1;
    }

    protected override void BeginEvent()
    {
        wait = 2f;
    }

    protected override void UpdateEvent() {
        wait -= Time.deltaTime;
        if (wait <= 0) this.End();
    }
}
