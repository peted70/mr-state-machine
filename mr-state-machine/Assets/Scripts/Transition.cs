using System.Collections.Generic;
using UnityEngine;
public abstract class Transition : MonoBehaviour
{
    public List<Trigger> triggers = new List<Trigger>();
    public StateBase targetState;

    public abstract StateBase IsCompleted();

    public virtual void Init(StateBase parent) { }

    public virtual void Reset() { }
}
