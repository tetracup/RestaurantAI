using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class WaiterAI : BTree
{
    Node root;
    Main.Tile IdleTile;

    protected override Node Setup()
    {
        IdleTile = Main.gridArray[13,1];

        root = 
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new WaitForCounterItem(gameObject, true),
                    new MoveTo(gameObject, true),
                    new PickUpItemFromCounter(gameObject),
                    new MoveTo(gameObject, true),
                    new GiveOrderToCustomer(gameObject),
                }),
                new Sequence(new List<Node>
                {
                    new WaitForOrder(gameObject),
                    new MoveTo(gameObject, true),
                    new PickUpOrder(gameObject),
                    new GoToReceiptCounter(gameObject),
                    new MoveTo(gameObject, true),
                    new PlaceOnCounter(gameObject)
                }),
            });

        
        return root;
    }



}
