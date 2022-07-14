using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class WaitFor : Node
{
    float timer = 0.0f;
    float timerMax;

    public WaitFor(float timer)
    {
        timerMax = timer;
    }

    public override NodeState Evaluate()
    {
        if(timer < timerMax)
        {
            timer += Time.deltaTime;
            state = NodeState.RUNNING;
        }
        else
        {
            ResetTimer();
            state = NodeState.SUCCESS;
        }
        return state;
    }

    void ResetTimer()
    {
        timer = 0.0f;
    }
}
