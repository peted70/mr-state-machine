using System;
using System.Collections;
using UnityEngine;

public class ExampleState : StateBase
{
    IEnumerator CallWithDelay(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public override void StateEnter()
    {
        base.StateEnter();
        Invoke("Complete", 2);
    }

    private void Complete()
    {
        Completed = true;
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}
