﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class StateMachineEditor : EditorWindow
{
    public StateMachineEditor()
    {

    }

    [SerializeField]
    private StateManager _stateMachine;

    //StateManager GetStateManager() { return _stateMachine; }
    //void SetStateManager(StateManager sm)
    //{
    //    if (sm == _stateMachine)
    //        return;
    //    if (_stateMachine != null)
    //        _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
    //    _stateMachine = sm;
    //    if (_stateMachine != null)
    //        _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;
    //}

    //private StateManager StateMachine
    //{
    //    get { return _stateMachine; }
    //    set
    //    {
    //        if (value == _stateMachine)
    //            return;
    //        if (_stateMachine != null)
    //            _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
    //        _stateMachine = value;
    //        if (_stateMachine != null)
    //            _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;
    //    }
    //}

    private void _stateMachine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Repaint();
    }

    private Vector2 scrollPos;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/State Machine Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        StateMachineEditor window = (StateMachineEditor)EditorWindow.GetWindow(typeof(StateMachineEditor));
        window.Show();

        //EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
    }

    
    private void OnFocus()
    {
        var selected = Selection.activeGameObject;
        StateManager sm = null;
        if (selected != null)
        {
            sm = selected.GetComponent<StateManager>();
        }

        //SetStateManager(sm);
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;
    }

    private void OnEnable()
    {
        var selected = Selection.activeGameObject;
        StateManager sm = null;
        if (selected != null)
        {
            sm = selected.GetComponent<StateManager>();
        }

        //SetStateManager(sm);
        _stateMachine = sm;
    }

    public void Awake()
    {
        // Find the currently selected GameObject...
        var selected = Selection.activeGameObject;
        StateManager sm = null;
        if (selected != null)
        {
            sm = selected.GetComponent<StateManager>();
        }

        //SetStateManager(sm);
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;
    }

    private static void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
    {
    }

    private void OnGUI()
    {
        Debug.Log("OnGUI");

        //Handles.BeginGUI();
        //Handles.DrawBezier(windowRect.center, windowRect2.center, new Vector2(windowRect.xMax + 50f, windowRect.center.y), new Vector2(windowRect2.xMin - 50f, windowRect2.center.y), Color.red, null, 5f);
        //Handles.EndGUI();
        if (_stateMachine == null)
        {
            var selected = Selection.activeGameObject;
            StateManager sm = null;
            if (selected != null)
            {
                sm = selected.GetComponent<StateManager>();
            }

            //SetStateManager(sm);
            _stateMachine = sm;
            if (_stateMachine != null)
                _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
            _stateMachine = sm;
            if (_stateMachine != null)
                _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;

        }

        if (_stateMachine == null || _stateMachine.States == null)
            return;

        if (Event.current.type == EventType.ContextClick)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Add State"), false, OnAddState);
            menu.ShowAsContext();

            Event.current.Use();
        }

        Draw();


        ProcessEvents(Event.current);

        if (GUI.changed)
            Repaint();

    }

    //private void Update()
    //{
    //    if (StateMachine == null || StateMachine.States == null)
    //        return;
    //    Repaint();
    //    //Draw();    
    //}

    private void Draw()
    {
        BeginWindows();

        GUI.backgroundColor = Color.red;
        GUIStyle gsTest = new GUIStyle();

        // GUILayout.BeginArea(new Rect(0, 0, position.width / 2, position.height));

        scrollPos = GUILayout.BeginScrollView(scrollPos, true, true);
        GUI.backgroundColor = Color.blue;

        foreach (var state in _stateMachine.States)
        {
            //GUILayout.Label("Hello", GUILayout.Width(2000), GUILayout.Height(2000));

            //Rect rect = GUILayoutUtility.GetRect(state.windowRect.width, state.windowRect.height);
            //GUILayout.BeginArea(state.windowRect);
            //GUILayout.Button("hello");
            //GUILayout.EndArea();
            if (state.IsCurrent)
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.blue;
            }

            GUILayoutUtility.GetRect(state.windowRect.width, state.windowRect.height);
            GUI.Box(state.windowRect, state.Name);
            //GUILayout.Box(state.Name, GUILayout.Width(state.windowRect.width), GUILayout.Height(state.windowRect.height));
        }

        GUILayout.EndScrollView();

        //GUILayout.EndArea();

        //GUILayout.BeginArea(new Rect(position.width / 2, 0, position.width / 2, position.height));
        //GUILayout.Button("Click me");
        //GUILayout.EndArea();

        EndWindows();
    }

    void OnSelectionChange()
    {
        // Find the currently selected GameObject...
        var selected = Selection.activeGameObject;
        StateManager sm = null;
        if (selected != null)
        {
            sm = selected.GetComponent<StateManager>();
        }

        //SetStateManager(sm);
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged -= _stateMachine_PropertyChanged;
        _stateMachine = sm;
        if (_stateMachine != null)
            _stateMachine.PropertyChanged += _stateMachine_PropertyChanged;

        Repaint();
    }

    private void ProcessEvents(Event current)
    {
        foreach (var state in _stateMachine.States)
        {
            state.ProcessEvents(current);
        }
    }
    private void OnAddState()
    {
        var sb = new StateBase();
        sb.Name = "New State";
        _stateMachine.States.Add(sb);
    }
}
