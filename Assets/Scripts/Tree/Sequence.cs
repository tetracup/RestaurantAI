using System.Collections.Generic;

namespace BehaviorTree
{
    //Given that no nodes are running or failure, sequence succeeds.
    //Else if there are nodes that are running but no failure, sequence is running.
    //Else if one of the nodes are failure, then sequence is failure
    public class Sequence : Node
    {
        int curIndex = 0;
        public Sequence() : base() { }

        public Sequence(Node child) : base(child) { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            switch (children[curIndex].Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    curIndex = 0;
                    break;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    if(curIndex == children.Count - 1)
                    {
                        state = NodeState.SUCCESS;
                        curIndex = 0;
                        break;
                    }
                    curIndex++;
                    state = NodeState.RUNNING;
                    break;
            }
            return state;
        }

    }

}
