using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class CheckNearestCounter : Node
{
    GameObject curGameObject;

    public CheckNearestCounter(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {


        //List<Main.Tile> openList = Main.CounterList;
        //Main.Tile closestCounter = FindClosestCounter(openList);
        //while(openList.Count != 0)
        //{
        //    if (Main.blackboard.ContainsKey("Counter" + Main.CounterList.IndexOf(closestCounter).ToString()))
        //    {
        //        openList.Remove(closestCounter);
        //        FindClosestCounter(openList);
        //    }
        //    else
        //        break;
        //}

        if(curGameObject.transform.GetChild(0).name == "TakeOut")
        {
            if (!Main.blackboard.ContainsKey("Counter" + (Main.CounterList.Count - 1)))
                Main.blackboard.Add("Counter" + (Main.CounterList.Count - 1), curGameObject.transform.GetChild(0));
            else
                Main.blackboard["Counter" + (Main.CounterList.Count - 1)] = curGameObject.transform.GetChild(0);

            if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                Main.blackboard.Add(curGameObject.name + "_targetTile", Main.CounterList[Main.CounterList.Count - 1]);
            else
                Main.blackboard[curGameObject.name + "_targetTile"] = Main.CounterList[Main.CounterList.Count - 1];

            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            //Cycles through all counters, if all are taken up action fails
            for (int i = 1; i < Main.CounterList.Count - 1; i++)
            {
                Main.Tile counter = Main.CounterList[i];
                if (Main.blackboard.ContainsKey("Counter" + Main.CounterList.IndexOf(counter).ToString()))
                {
                    continue;
                }
                else
                {
                    Main.blackboard.Add("Counter" + Main.CounterList.IndexOf(counter).ToString(), curGameObject.transform.GetChild(0));

                    if (!Main.blackboard.ContainsKey(curGameObject.name + "_targetTile"))
                        Main.blackboard.Add(curGameObject.name + "_targetTile", counter);
                    else
                        Main.blackboard[curGameObject.name + "_targetTile"] = counter;

                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }

        

        state = NodeState.FAILURE;
        return state;


    }

    Main.Tile FindClosestCounter(List<Main.Tile> openList)
    {
        Main.Tile closestCounter;
        closestCounter = Main.CounterList[0];
        foreach (Main.Tile counter in openList)
        {
            if (Vector2.Distance(curGameObject.transform.position, new Vector2(counter.pos.x + 0.5f, counter.pos.y - 0.5f)) < Vector2.Distance(curGameObject.transform.position, new Vector2(closestCounter.pos.x + 0.5f, closestCounter.pos.y - 0.5f)))
                closestCounter = counter;
        }
        return closestCounter;
    }
}
