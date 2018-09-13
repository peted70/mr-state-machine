using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class DelayState : StateBase
{
    [Range(0.0f, 300.0f)]
    public float WaitFor = 2.0f;

    IEnumerator CallWithDelay(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public override void StateEnter(StateManager manager)
    {
        base.StateEnter(manager);
        Invoke("Complete", WaitFor);
    }

    private void Complete()
    {
        Completed = true;
    }

    public override void StateExit(StateManager manager)
    {
        base.StateExit(manager);
    }
}
