using System;

public abstract class ConnectableState : IConnectableState
{
    protected readonly ConnectableComponentSphere ConnectableComponentSphereRef;
    protected static Action<ConnectableComponentSphere> OnSphereSelected = delegate { };
    protected bool ConnectionModeIsOn;
    protected static ConnectingLine ConnectingLine;

    protected ConnectableState(ConnectableComponentSphere componentSphere) {
        ConnectableComponentSphereRef = componentSphere;
    }

    public abstract void StartState();
    public abstract void ExitState();
    public virtual void OnMouseDown() { }
    public virtual void OnMouseDragging() { }
    public virtual void OnMouseOver() { }
    public virtual void OnMouseExit() { }
}
