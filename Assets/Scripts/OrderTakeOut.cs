using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class OrderTakeOut : Node
{
    Transform transform;
    GameObject customer;

    public OrderTakeOut(GameObject customer)
    {
        transform = customer.transform;
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        Vector2Int curPos = new Vector2Int((int)transform.position.x, (int)transform.position.y + 1);

        OrderVariables.OrderType newOrderType = OrderVariables.OrderType.Burger;
        switch (Random.Range(1, 4))
        {
            case 1:
                newOrderType = OrderVariables.OrderType.Burger;
                break;
            case 2:
                newOrderType = OrderVariables.OrderType.Pizza;
                break;
            case 3:
                newOrderType = OrderVariables.OrderType.Pasta;
                break;
        }

        GameObject newOrder = new GameObject("TakeOutOrder");
        newOrder.transform.parent = transform;
        newOrder.transform.localPosition = Vector2.zero;

        newOrder.AddComponent<OrderVariables>().SetVariables(Main.gridArray[curPos.x, curPos.y], newOrderType, customer, true);

        SpriteRenderer _spriteRend = newOrder.AddComponent<SpriteRenderer>();
        _spriteRend.sortingOrder = 2;
        _spriteRend.sprite = Resources.Load<Sprite>("OrderPaper");



        state = NodeState.SUCCESS;
        return state;


    }
}
