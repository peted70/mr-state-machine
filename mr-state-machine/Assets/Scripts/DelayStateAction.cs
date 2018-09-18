using System.Collections.Generic;
using UnityEngine;

public class DelayStateAction : StateAction
{
    [Range(0.0f, 300.0f)]
    public float WaitFor = 2.0f;

    private bool _completed;

    public override IEnumerable<StateAction> Children()
    {
        return null;
    }

    public override Transition IsCompleted()
    {
        return _completed ? transition : null;
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
