using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeShop : NodeElement
{
    private Shop shop;

    public override void Begin(){
        this.End();
        return;
    }
}
