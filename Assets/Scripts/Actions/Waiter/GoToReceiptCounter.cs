using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GoToReceiptCounter : Node
{
    GameObject curGameObject;
    // Start is called before the first frame update
    public GoToReceiptCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }

    public override NodeState Evaluate()
    {
        Main.blackboard[curGameObject.name + "_targetTile"] = Main.CounterList[0];
        Debug.Log(Main.CounterList[0].pos);

        state = NodeState.SUCCESS;
        return state;
    }
}
