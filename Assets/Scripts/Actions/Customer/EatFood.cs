using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class EatFood : Node
{
    GameObject curGameObject;

    public EatFood(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        GameObject.Destroy(curGameObject.transform.GetChild(0).gameObject);

        Main.Tile curTile = Main.gridArray[(int)curGameObject.transform.position.x, (int)(curGameObject.transform.position.y + 1)];
        curTile.Occupy(false);
        Main.chairList.Add(curTile);
        if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
            Main.blackboard.Add(curGameObject.name + "_targetTile", Main.gridArray[0, 11]);
        else
            Main.blackboard[curGameObject.name + "_targetTile"] = Main.gridArray[0, 11];

        state = NodeState.SUCCESS;
        return state;

        
    }
}
