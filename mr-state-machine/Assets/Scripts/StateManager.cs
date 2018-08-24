using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public List<StateBase> States;
    private StateBase _currentState;
    private int _index = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log("State Manager: Start");

        if (_currentState == null)
        {
            _currentState = States[_index];
            _currentState.StateEnter();
        }
    }

    private void GoToNextState()
    {
        Debug.Log("State Manager: GoToNextState");

        if (_currentState == null)
        {
            _currentState = States[_index];
            _currentState.StateEnter();
            return;
        }

        var prev = _currentState;
        _currentState = States[++_index];
        prev.StateExit();
        _currentState.StateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == null)
            return;

        if (_currentState.Completed)
            GoToNextState();
        else
            _currentState.UpdateState();
    }
}
