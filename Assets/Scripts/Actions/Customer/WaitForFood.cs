using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class WaitForFood : Node
{
    GameObject curGameObject;

    public WaitForFood(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        if (curGameObject.transform.childCount != 0 && (curGameObject.transform.GetChild(0).name == "Food" || curGameObject.transform.GetChild(0).name == "TakeOut"))
        {
            if(curGameObject.transform.GetChild(0).name == "TakeOut")
            {
                Main.blackboard.Remove("Counter" + (Main.CounterList.Count - 1));
                Main.takeOutBusy = false;
            }
            
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
