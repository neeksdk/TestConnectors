namespace ConnectableStates
{
    public class MovableStateBase : IMovable
    {
        public virtual void StartState() { }

        public virtual void ExitState() { }

        public virtual void OnMouseDown() { }

        public virtual void OnMouseUp() { }

        public virtual void OnMouseDragging() { }

        public virtual void OnMouseEnter() { }

        public virtual void OnMouseExit() { }

        public virtual void Update() { }
    }
}
