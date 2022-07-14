using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CleanCounter : Node
{
    GameObject curGameObject;
    bool assignedCurTile;
    Main.Tile curTile;
    public CleanCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        if (!assignedCurTile)
            curTile = (Main.Tile)Main.blackboard[curGameObject.name + "_targetTile"];

        if ((curTile.canBeCleaned() && Main.ReceiptsOnCounter.Count == 0) || curTile.isDirty())
        {
            curTile.ChangeCleanlinessOfCounter(-0.25f);
            state = NodeState.RUNNING;
            return state;
        }


        assignedCurTile = false;
        state = NodeState.SUCCESS;
        return state;
    }
}
