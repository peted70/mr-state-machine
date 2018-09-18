using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class StateMachineEditor : EditorWindow
{
    private Texture2D backgroundTexture;
    private GUIStyle textureStyle;

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
        backgroundTexture = Texture2D.whiteTexture;
        textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };

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
        DrawHandles();

        ProcessEvents(Event.current);

        if (GUI.changed)
            Repaint();

    }

    public void DrawRect(Rect position, Color color, GUIContent content = null)
    {
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(position, content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;
    }

    private void Draw()
    {
        BeginWindows();

        GUI.backgroundColor = Color.red;

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

            
            DrawRect(state.windowRect, state.IsCurrent ? Color.green : Color.blue, new GUIContent(state.Name));
            //GUI.Box(state.windowRect, state.Name);
            //GUILayout.Box(state.Name, GUILayout.Width(state.windowRect.width), GUILayout.Height(state.windowRect.height));
        }

        GUILayout.EndScrollView();

        //GUILayout.EndArea();

        //GUILayout.BeginArea(new Rect(position.width / 2, 0, position.width / 2, position.height));
        //GUILayout.Button("Click me");
        //GUILayout.EndArea();

        EndWindows();
    }

    public void DrawHandles()
    {
        foreach (var state in _stateMachine.States)
        {
            foreach (var action in state.Actions)
            {
                if (action == null)
                {
                    Debug.Log("Action is null");
                    continue;
                }
                var transition = action.transition;
                if (transition == null)
                    continue;

                Handles.BeginGUI();
                Handles.DrawBezier(new Vector3(state.windowRect.xMax, state.windowRect.center.y),
                    new Vector3(transition.targetState.windowRect.xMin, transition.targetState.windowRect.center.y),
                    new Vector2(state.windowRect.xMax + 50f, state.windowRect.center.y), 
                    new Vector2(transition.targetState.windowRect.xMin - 50f, transition.targetState.windowRect.center.y), 
                    Color.red, null, 5f);
                Handles.EndGUI();
            }
        }
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
