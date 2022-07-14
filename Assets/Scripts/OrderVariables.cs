using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderVariables : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isTakeOut = false;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public enum OrderType
    {
        Burger,
        Pasta,
        Pizza
    }
    public struct OrderInfo
    {
        public OrderType order;
        public Main.Tile customerTile;
    }

    public OrderInfo curOrder;
    public GameObject customerObj;

    public void SetVariables(Main.Tile customerTile, OrderType order, GameObject customerObj, bool isTakeOut)
    {
        this.isTakeOut = isTakeOut;
        curOrder.order = order;
        curOrder.customerTile = customerTile;
        this.customerObj = customerObj;
        Debug.Log("new order at pos (" + (int)customerObj.transform.position.x + ", " + (int)(customerObj.transform.position.y + 1) + "), with " + order.ToString());
    }
}
