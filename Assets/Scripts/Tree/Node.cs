using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;
        public Node parent;
        public Node rootNode;
        protected List<Node> children = new List<Node>();

        public Node()
        {
            parent = null;
        }

        public Node(Node node)
        {
            Attach(node);
        }

        public Node(List<Node> children)
        {
            Attach(children);
        }
        public void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
            
        }

        public void Attach(List<Node> children)
        {
            foreach(Node child in children)
            {
                Attach(child);
            }
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }

}
