using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckForDirtyCounters : Node
{
    GameObject curGameObject;
    bool threatLevel;
    List<Main.Tile> dirtyTiles;
    List<Main.Tile> cleanableTiles;
    Main.Tile curTile;

    public CheckForDirtyCounters(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {

        curTile = Main.FoodCountersArray[0, 0];
        foreach(Main.Tile counter in Main.FoodCountersArray)
        {
            if (Main.gridArray[counter.pos.x, counter.pos.y].IsMoreDirtyThan(Main.gridArray[curTile.pos.x, curTile.pos.y]))
                curTile = counter;
        }
        int posX = curTile.pos.x;
        int posY = curTile.pos.y;

        if ((Main.gridArray[posX, posY].canBeCleaned() && Main.ReceiptsOnCounter.Count == 0) || Main.gridArray[posX, posY].isDirty())
        {
            if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                Main.blackboard.Add(curGameObject.name + "_targetTile", Main.gridArray[posX, posY]);
            else
                Main.blackboard[curGameObject.name + "_targetTile"] = Main.gridArray[posX, posY];

            state = NodeState.SUCCESS;
            return state;
        }


        //If no dirty counters, no cleaning needed
        state = NodeState.FAILURE;
        return state;
    }
}