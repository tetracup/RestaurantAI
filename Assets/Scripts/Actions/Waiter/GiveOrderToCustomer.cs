using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class GiveOrderToCustomer : Node
{
    GameObject curGameObject;

    public GiveOrderToCustomer(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Transform orderTransform = curGameObject.transform.GetChild(0).transform;
        GameObject customerObj = orderTransform.gameObject.GetComponent<OrderVariables>().customerObj;
        orderTransform.parent = customerObj.transform;
        orderTransform.position = customerObj.transform.position;

        state = NodeState.SUCCESS;
        return state;
    }
}