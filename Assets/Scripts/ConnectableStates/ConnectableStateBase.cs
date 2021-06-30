using System;
using ConnectableComponents;

namespace ConnectableStates
{
    public abstract class ConnectableStateBase : IConnectableState
    {
        protected readonly ConnectableComponentSphere ConnectableComponentSphereRef;
        protected static Action<ConnectableComponentSphere> OnSphereSelected = delegate { };
        protected bool ConnectionModeIsOn;
        protected static ConnectingLine ConnectingLine;

        protected ConnectableStateBase(ConnectableComponentSphere componentSphere) {
            ConnectableComponentSphereRef = componentSphere;
        }

        protected bool IsSameConnectingLine() {
            return ConnectingLine == ConnectableComponentSphereRef.GetConnectingLine();
        }

        public abstract void StartState();
        public abstract void ExitState();
        public virtual void OnMouseDown() { }
        public virtual void OnMouseUp() { }
        public virtual void OnMouseDragging() { }
        public virtual void OnMouseEnter() { }
        public virtual void OnMouseExit() { }
        public virtual void Update() { }
    }
}
