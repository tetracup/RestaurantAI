using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class PickUpItemFromCounter : Node
{
    GameObject curGameObject;

    public PickUpItemFromCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Transform orderTransform;
        orderTransform = (Transform)Main.GetData(curGameObject.name + "_targetItem");
        Main.blackboard.Remove(curGameObject.name + "_targetItem");
        orderTransform.parent = curGameObject.transform;
        orderTransform.position = curGameObject.transform.position;

        if (orderTransform.name == "Order" ||orderTransform.name == "TakeOutOrder")
            Main.ReceiptsOnCounter.Remove(orderTransform);
        Main.blackboard.Remove("Counter" + Main.CounterList.IndexOf((Main.Tile)Main.blackboard[curGameObject.name + "_targetTile"]));
        if (orderTransform.name == "Food")
        {
            Vector2 customerPosition = orderTransform.gameObject.GetComponent<OrderVariables>().customerObj.transform.position;
            if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                Main.blackboard.Add(curGameObject.name + "_targetTile", Main.gridArray[(int)customerPosition.x, (int)customerPosition.y + 1]);
            else
                Main.blackboard[curGameObject.name + "_targetTile"] = Main.gridArray[(int)customerPosition.x, (int)customerPosition.y + 1];
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
