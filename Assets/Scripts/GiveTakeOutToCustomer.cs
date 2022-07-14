using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class GiveTakeOutToCustomer : Node
{
    GameObject curGameObject;

    public GiveTakeOutToCustomer(GameObject curGameObject)
    {
        this.curGameObject = curGameObject;
    }
    public override NodeState Evaluate()
    {
        Transform orderTransform = curGameObject.transform.GetChild(0).transform;
        OrderVariables orderInfo = orderTransform.gameObject.GetComponent<OrderVariables>();
        if(!orderInfo.isTakeOut)
        {
            state = NodeState.FAILURE;
            return state;
        }
        orderTransform.parent = orderInfo.customerObj.transform;
        orderTransform.position = orderInfo.customerObj.transform.position;

        state = NodeState.SUCCESS;
        return state;
    }
}
