using System;
using ConnectableStates;
using UnityEngine;

namespace ConnectableComponents
{
    public class ConnectableComponentSphere : ConnectableComponent
    {
        private IConnectableState _connectableStateStatic;
        private IConnectableState _connectableStateDynamic;
        private IConnectableState _connectableStateTwoMethods;
        private IConnectableState _connectableStateNone;

        private void Start() {
            _connectableStateStatic = new ConnectableStateStatic(this);
            _connectableStateDynamic = new ConnectableStateDynamic(this);
            _connectableStateTwoMethods = new ConnectableStateTwoMethods(this);
            _connectableStateNone = new ConnectableStateNone(this);
            ChangeState(_connectableStateTwoMethods);

            TestMenu.OnUIPressed += OnUIPressed;
        }

        private void OnDestroy() {
            TestMenu.OnUIPressed -= OnUIPressed;
        }
        
        public ConnectingLine GetConnectingLine() {
            return connectingLine;
        }

        public void SetMaterialColor(Color color) {
            ConnectableMeshRender.material.color = color;
        }

        private void OnMouseDown() {
            CurrentState.OnMouseDown();
        }

        private void OnMouseUp() {
            CurrentState.OnMouseUp();
        }

        private void OnMouseEnter() {
            CurrentState.OnMouseEnter();
        }

        private void OnMouseExit() {
            CurrentState.OnMouseExit();
        }

        private void OnMouseDrag() {
            CurrentState.OnMouseDragging();
        }

        private void ClearConnection() {
            connectingLine.SetStartPoint(transform);
            connectingLine.SetEndPoint(transform);
        }

        private void Update() {
            CurrentState.Update();
        }

        private void OnUIPressed(TestMenu.UICommandType commandType) {
            switch (commandType) {
                case TestMenu.UICommandType.Method1:
                    ChangeState(_connectableStateStatic);
                    break;
                case TestMenu.UICommandType.Method2:
                    ChangeState(_connectableStateDynamic);
                    break;
                case TestMenu.UICommandType.TwoMethods:
                    ChangeState(_connectableStateTwoMethods);
                    break;
                case TestMenu.UICommandType.NoneMethods:
                    ChangeState(_connectableStateNone);
                    break;
                case TestMenu.UICommandType.ClearConnections:
                    ClearConnection();
                    break;
                case TestMenu.UICommandType.Randomize:
                    break;
                default:
                    break;
            }
        }
    }
}
