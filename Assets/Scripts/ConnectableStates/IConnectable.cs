public interface IConnectableState
{
    void StartState();
    void ExitState();
    void OnMouseDown();
    void OnMouseDragging();
    void OnMouseEnter();
    void OnMouseExit();
    void Update();
}