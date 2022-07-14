using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckReceiptCounter : Node
{
    GameObject curGameObject;
    // Start is called before the first frame update
    public CheckReceiptCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }

    public override NodeState Evaluate()
    {
        if(Main.ReceiptsOnCounter.Count == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }    
        if(Main.ReceiptsOnCounter[0].name == "Order")
            Main.blackboard[curGameObject.name + "_targetTile"] = Main.CounterList[0];
        else
            Main.blackboard[curGameObject.name + "_targetTile"] = Main.CounterList[Main.CounterList.Count-1];

        if (Main.blackboard.ContainsKey(curGameObject.name + "_targetItem"))
            Main.blackboard[curGameObject.name + "_targetItem"] = Main.ReceiptsOnCounter[0];
        else
            Main.blackboard.Add(curGameObject.name + "_targetItem", Main.ReceiptsOnCounter[0]);

        Main.ReceiptsOnCounter.Remove(Main.ReceiptsOnCounter[0]);

        state = NodeState.SUCCESS;
        return state;
    }
}
