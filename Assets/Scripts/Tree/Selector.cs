using System.Collections.Generic;

namespace BehaviorTree
{
    //Given that one of the nodes are a success, selector succeeds.
    //Else if there are no nodes are a success, but one is running, selector is running.
    //Else if all nodes are failure, then selector is failure
    public class Selector : Node
    {
        int curIndex = 0;
        public Selector() : base() { }
        public Selector(Node child) : base(child) { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            switch(children[curIndex].Evaluate())
            {
                case NodeState.FAILURE:
                    if(curIndex == children.Count - 1)
                    {
                        state = NodeState.FAILURE;
                        curIndex = 0;
                        break;
                    }
                    state = NodeState.RUNNING;
                    curIndex++;
                    break;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    curIndex = 0;
                    break;
            }
            return state;

            //switch (children[curIndex].Evaluate())
            //{
            //    case NodeState.FAILURE:
            //        if (curIndex == children.Count - 1)
            //        {
            //            state = NodeState.FAILURE;
            //            curIndex = 0;
            //            return state;
            //        }
            //        curIndex++;
            //        state = NodeState.RUNNING;
            //        return state;
            //    case NodeState.RUNNING:
            //        state = NodeState.RUNNING;
            //        return state;
            //    case NodeState.SUCCESS:
            //        state = NodeState.SUCCESS;
            //        curIndex = 0;
            //        return state;
            //}
            //state = NodeState.FAILURE;
            //return state;
        }

        /*
          public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
         */
    }

}
