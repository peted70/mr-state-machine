using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTransition : Transition
{
    [Range(0.0f, 300.0f)]
    public float WaitFor = 2.0f;

    private bool _completed;

    public override StateBase IsCompleted()
    {
        return _completed ? targetState : null;
    }

    public override void Init(StateBase parent)
    {
        Invoke("Complete", WaitFor);
    }

    private void Complete()
    {
        _completed = true;
    }
    public override void Reset()
    {
        _completed = false;
    }
}

