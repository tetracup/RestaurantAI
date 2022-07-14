using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class WaitForCounterItem : Node
{
    GameObject curGameObject;
    bool lookingForFood;

    public WaitForCounterItem(GameObject curGameObject, bool lookingForFood)
    {
        this.curGameObject = curGameObject;
        this.lookingForFood = lookingForFood;
    }
    public override NodeState Evaluate()
    {
        //Debug.Log("counteritem");
        if(!lookingForFood && Main.ReceiptsOnCounter.Count != 0)
        {
            if (Main.blackboard.ContainsKey(curGameObject.name + "_targetItem"))
                Main.blackboard[curGameObject.name + "_targetItem"] = Main.ReceiptsOnCounter[0];
            else
                Main.blackboard.Add(curGameObject.name + "_targetItem", Main.ReceiptsOnCounter[0]);

            if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                Main.blackboard.Add(curGameObject.name + "_targetTile", Main.CounterList[0]);
            else
                Main.blackboard[curGameObject.name + "_targetTile"] = Main.CounterList[0];

            state = NodeState.SUCCESS;
            return state;
        }
        else if(lookingForFood) for(int i = 1; i < Main.CounterList.Count; i++)
        {
            Main.Tile counter = Main.CounterList[i];
            string orderKey = "Counter" + Main.CounterList.IndexOf(counter).ToString();

            if (Main.blackboard.TryGetValue(orderKey, out object obj))
            {
                if (obj == null)
                    continue;
                Transform transformItem = (Transform)Main.GetData(orderKey);
                if (Main.gridArray[(int)transformItem.position.x, (int)transformItem.position.y + 1].pos != counter.pos)
                    continue;

                if ((transformItem.name == "Order" && lookingForFood) || (transformItem.name == "Food" && !lookingForFood) || transformItem.GetComponent<OrderVariables>().isTakeOut)
                    continue;
                Main.Tile targetOrderTile = Main.gridArray[(int)transformItem.position.x, (int)(transformItem.position.y + 1)];

                if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                    Main.blackboard.Add(curGameObject.name + "_targetTile", targetOrderTile);
                else
                    Main.blackboard[curGameObject.name + "_targetTile"] = targetOrderTile;


                if (Main.blackboard.ContainsKey(curGameObject.name + "_targetItem"))
                    Main.blackboard[curGameObject.name + "_targetItem"] = transformItem;
                else
                    Main.blackboard.Add(curGameObject.name + "_targetItem", transformItem);

                    Debug.Log("counteritemsuccess");
                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }

    
}
