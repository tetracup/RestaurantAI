using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class MakeFood : Node
{
    GameObject curGameObject;
    float cookingTime;
    float cookingTimerMax;

    public MakeFood(GameObject curGameObject, float cookingTimerMax)
    {
        this.curGameObject = curGameObject;
        this.cookingTimerMax = cookingTimerMax;
    }
    public override NodeState Evaluate()
    {
        if(cookingTime <= cookingTimerMax)
        {
            cookingTime += Time.deltaTime;
            ((Main.Tile)Main.blackboard[curGameObject.name + "_targetTile"]).ChangeCleanlinessOfCounter(0.14f);
            state = NodeState.RUNNING;
            return state;
        }
        OrderVariables orderInfo = curGameObject.transform.GetChild(0).GetComponent<OrderVariables>();
        if (!orderInfo.isTakeOut)
            orderInfo.name = "Food";
        else
            orderInfo.name = "TakeOut";
        
        orderInfo.spriteRenderer.sprite = Resources.Load<Sprite>(orderInfo.curOrder.order.ToString());

        cookingTime = 0.0f;
        state = NodeState.SUCCESS;
        return state;
    }
}