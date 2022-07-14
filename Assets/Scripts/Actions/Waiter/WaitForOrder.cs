using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class WaitForOrder : Node
{
    GameObject curGameObject;
    public WaitForOrder(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        foreach (GameObject customer in Main.CustomerList)
        {
            if(customer.transform.childCount != 0 && customer.transform.GetChild(0).name == "Order")
            {
                if (Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                    Main.blackboard[curGameObject.name + "_targetTile"] = Main.gridArray[(int)customer.transform.position.x, (int)customer.transform.position.y + 1];
                else
                    Main.blackboard.Add(curGameObject.name + "_targetTile", Main.gridArray[(int)customer.transform.position.x, (int)customer.transform.position.y + 1]);

                if (Main.blackboard.ContainsKey(curGameObject.name + "_targetItem"))
                    Main.blackboard[curGameObject.name + "_targetItem"] = customer.transform.GetChild(0);
                else
                    Main.blackboard.Add(curGameObject.name + "_targetItem", customer.transform.GetChild(0));

                state = NodeState.SUCCESS;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;



    }
}
