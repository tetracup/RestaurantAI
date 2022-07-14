using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class PickUpOrder : Node
{
    GameObject curGameObject;
    public PickUpOrder(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Transform orderTransform = (Transform)Main.GetData(curGameObject.name + "_targetItem");
        orderTransform.parent = curGameObject.transform;
        orderTransform.position = curGameObject.transform.position;
        state = NodeState.SUCCESS;
        return state;

    }
}
