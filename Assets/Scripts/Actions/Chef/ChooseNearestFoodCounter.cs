using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class ChooseNearestFoodCounter : Node
{
    GameObject curGameObject;

    public ChooseNearestFoodCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        
        OrderVariables orderInfo = curGameObject.transform.GetChild(0).GetComponent<OrderVariables>();
        Main.Tile firstCounter = Main.FoodCountersArray[(int)orderInfo.curOrder.order, 0];
        Main.Tile secondCounter = Main.FoodCountersArray[(int)orderInfo.curOrder.order, 1];
        Main.Tile closestCounter;
        if (Vector2.Distance(curGameObject.transform.position, new Vector2(firstCounter.pos.x + 0.5f, firstCounter.pos.y - 0.5f)) < Vector2.Distance(curGameObject.transform.position, new Vector2(secondCounter.pos.x + 0.5f, secondCounter.pos.y - 0.5f)))
            closestCounter = firstCounter;
        else
            closestCounter = secondCounter;

        if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
            Main.blackboard.Add(curGameObject.name + "_targetTile", Main.gridArray[closestCounter.pos.x, closestCounter.pos.y]);
        else
            Main.blackboard[curGameObject.name + "_targetTile"] = Main.gridArray[closestCounter.pos.x, closestCounter.pos.y];

        state = NodeState.SUCCESS;
        return state;
    }
}