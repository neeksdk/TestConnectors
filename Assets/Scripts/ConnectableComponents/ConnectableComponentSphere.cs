using System;
using UnityEngine;

public class ConnectableComponentSphere : ConnectableComponent
{
    private IConnectableState _connectableStateStatic;
    private IConnectableState _connectableStateDynamic;
    private IConnectableState _currentState;

    private void Start() {
        _connectableStateStatic = new ConnectableStateStatic(this);
        _connectableStateDynamic = new ConnectableStateDynamic(this);
        ChangeState(_connectableStateStatic);

        TestMenu.OnUIPressed += OnUIPressed;
    }

    private void OnDestroy() {
        TestMenu.OnUIPressed -= OnUIPressed;
    }

    private void ChangeState(IConnectableState newState) {
        if (_currentState == newState) return;
        
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

    private void OnMouseEnter() {
        _currentState.OnMouseEnter();
    }

    private void OnMouseExit() {
        _currentState.OnMouseExit();
    }

    private void Update() {
        _currentState.Update();
    }

    private void OnUIPressed(TestMenu.UICommandType commandType) {
        switch (commandType) {
            case TestMenu.UICommandType.Method1:
                ChangeState(_connectableStateStatic);
                break;
            case TestMenu.UICommandType.Method2:
                ChangeState(_connectableStateDynamic);
                break;
            default:
                break;
        }
    }
}
