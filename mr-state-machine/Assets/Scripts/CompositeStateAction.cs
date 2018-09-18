using System.Collections.Generic;
using System.Linq;

public class CompositeStateAction : StateAction
{
    public List<StateAction> children = new List<StateAction>();

    public override IEnumerable<StateAction> Children()
    {
        return children;
    }

    public override bool IsCompleted()
    {
        // Return the parent transition - if you want different behaviour use seperate actions
        bool allComplete = children.All(c => c.IsCompleted());
        return allComplete;
    }

    public override void Init(StateBase parent)
    {
        foreach (var child in children)
        {
            child.Init(parent);
        }
    }

    public override void Reset()
    {
        foreach (var child in children)
        {
            child.Reset();
        }
    }
}
