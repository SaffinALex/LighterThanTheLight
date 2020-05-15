using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeShop : NodeElement
{
    private Shop shop;

    public override void InitializeNode(float score = 0) {
        scoreDifficulty = score;
    }

    public override void Begin(){
        this.End();
        return;
    }
}
