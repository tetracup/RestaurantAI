using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class CheckForEmptyChairs : Node
{
    GameObject curGameObject;
    public CheckForEmptyChairs(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        if(curGameObject == null || Main.chairList.Count == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }
        int randIndex = Random.Range(0, Main.chairList.Count);
        Main.Tile chair = Main.chairList[randIndex];
        if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
            Main.blackboard.Add(curGameObject.name + "_targetTile", null);
        Main.blackboard[curGameObject.name + "_targetTile"] = chair;
        chair.isOccupied = true;
        Main.chairList.Remove(Main.chairList[randIndex]);
        state = NodeState.SUCCESS;
        return state;
    }
}
