using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //Base Class for Behaviour Trees 
    public abstract class BTree : MonoBehaviour
    {
        private Node _root = null;

        

        protected void Start() => _root = Setup();

        private void Update()
        {
            if (gameObject == null)
                return;
            if (_root != null)
                _root.Evaluate();
                
        }

        protected abstract Node Setup();

    }

}
