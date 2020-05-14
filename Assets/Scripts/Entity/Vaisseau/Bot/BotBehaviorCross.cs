using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviorCross : EntitySpaceShipBehavior
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        r2d = GetComponent<Rigidbody2D>();
        type = "BotCross";
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (!needGoAway) move();
        else GoAwayMove();
    }

    public override string getType()
    {
        type = "BotCross";
        return type;
    }

    public override void initialize()
    {
        life = 6;
    }

    public override void move()
    {
        r2d.velocity = -transform.up * speedMove;
    }
}
