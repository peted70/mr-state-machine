﻿using System;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Trigger))]

[Serializable]
public class StateBase : MonoBehaviour
{
    [SerializeField]
    private List<Transition> transitions = new List<Transition>();

    [HideInInspector]
    public bool IsCurrent = false;

    [SerializeField]
    public Rect windowRect = new Rect(100, 100, 200, 120);

    public StateBase()
    {
        Name = GetType().ToString();
    }

    [HideInInspector]
    public bool Completed = false;

    [SerializeField]
    public string Name;

    private bool _dragging;

    public virtual void StateEnter(StateManager manager)
    {
        IsCurrent = true;
        Debug.Log("State Enter: " + Name);
        Completed = false;
        gameObject.SetActive(true);
    }

    public virtual void StateExit(StateManager manager)
    {
        IsCurrent = false;
        Debug.Log("State Exit: " + Name);
        gameObject.SetActive(false);
    }

    public virtual void UpdateState(StateManager manager)
    {

    }

    public void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (windowRect.Contains(e.mousePosition))
                {
                    _dragging = true;
                    e.Use();
                }
                break;
            case EventType.MouseDrag:
                if (_dragging)
                {
                    windowRect.position += e.delta;
                    GUI.changed = true;
                    e.Use();
                }
                break;
            case EventType.MouseUp:
                if (_dragging)
                {
                    _dragging = false;
                    e.Use();
                }
                break;


            //case EventType.MouseDrag:
            //    windowRect.position += e.delta;
            //    e.Use();
            //    break;

        }
    }
}
