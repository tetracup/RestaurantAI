using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class MoveTo : Node
{
    AStarPathfind pathfind = new AStarPathfind();
    List<Main.Tile> curPath;
    Transform curTransform;
    Main.Tile targetTile;
    Vector2Int curPos;
    Vector3 targetDir;
    bool targetNextTo;
    bool wasTileGiven = true;
    bool checkIfOccupy;
    float speed;
    
    public MoveTo(GameObject obj, Main.Tile targetTile, bool targetNextTo)
    {
        this.curTransform = obj.transform;
        this.targetTile = targetTile;
        
        this.targetNextTo = targetNextTo;
        SetSpeed();
    }

    public MoveTo(GameObject obj, bool targetNextTo)
    {
        this.curTransform = obj.transform;
        this.targetNextTo = targetNextTo;
        wasTileGiven = false;
        SetSpeed();
    }

    public override NodeState Evaluate()
    {
        if (curPath == null)
        {
            if(!wasTileGiven)
                targetTile = (Main.Tile)Main.GetData(curTransform.name + "_targetTile");

            if(targetTile.pos == Main.gridArray[(int)curTransform.position.x, (int)curTransform.position.y + 1].pos)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            Main.Tile curTile = Main.gridArray[(int)this.curTransform.position.x, (int)(this.curTransform.position.y + 1f)];
            curPath = pathfind.FindPath(curTile, targetTile);


            if (curPath.Count == 0)
            {
                state = NodeState.SUCCESS;
                curPath = null;
                return state;
            }

            if(targetNextTo)
            {
                curPath.Remove(curPath[curPath.Count - 1]);
                if (curPath.Count == 0)
                {
                    state = NodeState.SUCCESS;
                    curPath = null;
                    return state;
                }
            }
                

            targetDir = Vector3.Normalize(new Vector3(curPath[0].pos.x + 0.5f, curPath[0].pos.y - 0.5f) - curTransform.position);
        }

        if(curPath.Count != 0)
        {
            if (CheckBounds())
            {
                curPath.Remove(curPath[0]);
                if(curPath.Count != 0)
                    targetDir = Vector3.Normalize(new Vector3(curPath[0].pos.x + 0.5f, curPath[0].pos.y - 0.5f) - curTransform.position);
            }
        }
        if (curPath.Count == 0)
        {
            state = NodeState.SUCCESS;
            curPath = null;
            return state;
        }
        
        curTransform.position += targetDir * speed * Time.deltaTime * 2.0f;

        state = NodeState.RUNNING;
        return state;
    }

    bool CheckBounds()
    {
        return
            targetDir.x >= 0 && curTransform.position.x > curPath[0].pos.x + 0.5f
            ||
            targetDir.x < 0 && curTransform.position.x < curPath[0].pos.x + 0.5f
            ||
            targetDir.y >= 0 && curTransform.position.y > curPath[0].pos.y - 0.5f
            ||
            targetDir.y < 0 && curTransform.position.y < curPath[0].pos.y - 0.5f;
    }
    void SetSpeed()
    {
        switch(curTransform.name)
        {
            case "Waiter":
                speed = 1.25f;
                break;
            case "Chef":
                speed = 1.25f;
                break;
            default:
                speed = 1.0f;
                break;
        }    
    }
}


