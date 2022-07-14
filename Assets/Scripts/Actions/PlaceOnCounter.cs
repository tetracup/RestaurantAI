using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class PlaceOnCounter : Node
{
    GameObject curGameObject;
    public PlaceOnCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Main.Tile counterTile;
        

        Transform orderTransform = curGameObject.transform.GetChild(0);
        orderTransform.transform.parent = null;
        if (orderTransform.name == "Order")
        {
            Main.ReceiptsOnCounter.Add(orderTransform);
            counterTile = Main.CounterList[0];
        }
        else if (orderTransform.name == "TakeOutOrder")
        {
            Main.ReceiptsOnCounter.Add(orderTransform);
            counterTile = Main.CounterList[Main.CounterList.Count-1];
        }
            counterTile = (Main.Tile)Main.GetData(curGameObject.name + "_targetTile");
        
        Vector2 counterPos = counterTile.pos;
        orderTransform.position = new Vector2(counterPos.x + 0.5f, counterPos.y - 0.5f);


        state = NodeState.SUCCESS;
        return state;

    }
}
