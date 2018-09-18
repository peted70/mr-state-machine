using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : MonoBehaviour
{
    public Transition transition;

    public abstract bool IsCompleted();

    public virtual void Init(StateBase parent) { }

    public virtual void Reset() { }

    public abstract IEnumerable<StateAction> Children();
}