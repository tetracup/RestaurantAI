using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class Leave : Node
{
    GameObject curGameObject;

    public Leave(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Main.CustomerList.Remove(curGameObject);
        GameObject.Destroy(curGameObject);
        state = NodeState.SUCCESS;
        return state;


    }
}