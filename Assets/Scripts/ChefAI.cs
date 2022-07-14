using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class ChefAI : BTree
{
    Node root;
    Main.Tile targetChair;

    protected override Node Setup()
    {
        root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckForDirtyCounters(gameObject),
                new MoveTo(gameObject, true),
                new CleanCounter(gameObject)
            }),
            new Sequence(new List<Node>
            {
                new CheckReceiptCounter(gameObject),
                new MoveTo(gameObject, true),
                new PickUpItemFromCounter(gameObject),
                new ChooseNearestFoodCounter(gameObject),
                new MoveTo(gameObject, true),
                new MakeFood(gameObject, 1.0f),
                new CheckNearestCounter(gameObject),
                new MoveTo(gameObject,true),
                new Selector(new List<Node>
                {
                    new GiveTakeOutToCustomer(gameObject),
                    new PlaceOnCounter(gameObject)
                })

            }),
            new MoveTo(gameObject, Main.gridArray[4,5], false)
        });

        return root;
    }
}
