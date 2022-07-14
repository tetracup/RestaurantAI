using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class GoToTakeOutCounter : Node
{
    GameObject curGameObject;
    public GoToTakeOutCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        if (curGameObject == null || Main.takeOutBusy || Main.blackboard.ContainsKey("Counter" + (Main.CounterList.Count - 1)))
        {
            state = NodeState.FAILURE;
            return state;
        }
        float randVal = Random.value;
        float customerMultiplier = (Main.CustomerList.Count / 24) * 0.75f;
        if(randVal > 0.25f + customerMultiplier )
        {
            Debug.Log(randVal);
            state = NodeState.FAILURE;
            return state;
        }
        Main.takeOutBusy = true;
            


        Main.Tile targetTile = Main.CounterList[Main.CounterList.Count-1];

        if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
            Main.blackboard.Add(curGameObject.name + "_targetTile", null);
        Main.blackboard[curGameObject.name + "_targetTile"] = targetTile;

        if (!Main.blackboard.ContainsKey("Counter" + (Main.CounterList.Count - 1)))
            Main.blackboard.Add("Counter" + (Main.CounterList.Count - 1), null);
        state = NodeState.SUCCESS;
        return state;
    }
}
