using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class StateManager : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField]
    public List<StateBase> States;

    [SerializeField]
    private StateBase _currentState;

    public StateBase CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState.GetInstanceID() == value.GetInstanceID())
                return;
            _currentState = value;
            FirePropertyChanged("CurrentState");
        }
    }

    [SerializeField]
    private int _index = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public void FirePropertyChanged(string propertyName)
    {
        if (PropertyChanged == null)
            return;
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("State Manager: Start");

        if (_currentState == null)
        {
            _currentState = States[_index];
            _currentState.StateEnter(this);
        }
    }

    private void GoToNextState()
    {
        Debug.Log("State Manager: GoToNextState");

        if (_currentState == null)
        {
            CurrentState = States[_index];
            CurrentState.StateEnter(this);
            return;
        }

        var prev = _currentState;
        CurrentState = States[++_index % States.Count];
        prev.StateExit(this);
        _currentState.StateEnter(this);
    }

    private void GoToState(StateBase state)
    {
        int idx = States.IndexOf(state);
        if (idx == -1)
            throw new Exception("Error: Trying to go to unknown state");
        _index = idx;

        if (_currentState == null)
        {
            CurrentState = state;
            CurrentState.StateEnter(this);
            return;
        }

        var prev = _currentState;
        CurrentState = state;
        prev.StateExit(this);
        _currentState.StateEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == null)
            return;

        var state = _currentState.IsCompleted();

        if (state != null)
        {
            GoToState(state);
        }
        else
        {
            _currentState.UpdateState(this);
        }
    }
}
