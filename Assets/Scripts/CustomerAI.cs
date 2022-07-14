using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class CustomerAI : BTree
{
    Node root;
    Main.Tile targetChair;

    protected override Node Setup()
    {
        root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new GoToTakeOutCounter(gameObject),
                new MoveTo(gameObject, true),
                new OrderTakeOut(gameObject),
                new PlaceOnCounter(gameObject),
                new WaitForFood(gameObject),
                new MoveTo(gameObject, Main.gridArray[0, 11], false),
                new Leave(gameObject)
            }),
            new Sequence(new List<Node>
            {
                new CheckForEmptyChairs(gameObject),
                new MoveTo(gameObject, false),
                new OccupyAndOrder(gameObject),
                new WaitForFood(gameObject),
                new WaitFor(2.0f),
                new EatFood(gameObject),
                new MoveTo(gameObject, false),
                new Leave(gameObject)
            })
        });


        return root;
    }
}
