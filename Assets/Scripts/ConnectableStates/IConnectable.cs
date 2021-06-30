namespace ConnectableStates
{
    public interface IConnectableState
    {
        void StartState();
        void ExitState();
        void OnMouseDown();
        void OnMouseUp();
        void OnMouseDragging();
        void OnMouseEnter();
        void OnMouseExit();
        void Update();
    }
}