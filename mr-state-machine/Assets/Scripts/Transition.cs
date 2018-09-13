using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : ScriptableObject
{
    public List<Trigger> triggers = new List<Trigger>();
    public StateBase targetState;
}
