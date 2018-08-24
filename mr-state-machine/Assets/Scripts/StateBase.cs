using UnityEngine;

public class StateBase : MonoBehaviour
{
    public StateBase()
    {
        Name = GetType().ToString();
    }

    [HideInInspector]
    public bool Completed = false;

    protected string Name;

    public virtual void StateEnter()
    {
        Debug.Log("State Enter: " + Name);
        Completed = false;
        gameObject.SetActive(true);
    }

    public virtual void StateExit()
    {
        Debug.Log("State Exit: " + Name);
        gameObject.SetActive(false);
    }

    public virtual void UpdateState()
    {

    }
}
