using UnityEngine;

public class ConnectableComponentSphere : ConnectableComponent
{
    private IConnectableState _staticState;
    private IConnectableState _currentState;

    private void Start() {
        ChangeState(new ConnectableStateStatic(this));
    }

    public void ChangeState(IConnectableState newState) {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.StartState();
    }
    
    public ConnectingLine GetConnectingLine() {
        return connectingLine;
    }

    public void SetMaterialColor(Color color) {
        ConnectableMeshRender.material.color = color;
    }

    private void OnMouseDown() {
        _currentState.OnMouseDown();
    }

    private void OnMouseOver() {
        _currentState.OnMouseOver();
    }

    private void OnMouseExit() {
        _currentState.OnMouseExit();
    }
}
