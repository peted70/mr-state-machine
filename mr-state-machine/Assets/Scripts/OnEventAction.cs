using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventActionType
{
    Awake,
    FixedUpdate,
    LateUpdateEvent,
    AnimatorIK,
    AnimatorMove,
    ApplicationFocus,
    ApplicationPause,
    ApplicationQuit,
    AudioFilterRead,
    BecameInvisible,
    BecameVisible,
    CollisionEnter,
    CollisionEnter2D,
    CollisionExit,
    CollisionExit2D,
    CollisionStay,
    CollisionStay2D,
    ConnectedToServer,
    ControllerColliderHit,
    Destroy,
    Disable,
    DrawGizmos,
    DrawGizmosSelected,
    Enable,
    GUI,
    JointBreak,
    LevelWasLoaded,
    MouseDown,
    MouseDrag,
    MouseEnter,
    MouseExit,
    MouseOver,
    MouseUp,
    MouseUpAsButton,
    ParticleCollision,
    PostRender,
    PreCull,
    PreRender,
    RenderImage,
    RenderObject,
    ServerInitialized,
    TriggerEnter,
    TriggerEnter2D,
    TriggerExit,
    TriggerExit2D,
    TriggerStay,
    TriggerStay2D,
    Validate,
    WillRenderObject,
    Reset,
    Start,
    Update
}

public class OnEventAction : StateAction
{
    public UnityEvent<Collision> ONCOLLISION;
    public EventActionType _event;

    public void OnCollisionEnter(Collision collision)
    {
        ONCOLLISION.Invoke(collision);
    }

    public override IEnumerable<StateAction> Children()
    {
        return null;
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
