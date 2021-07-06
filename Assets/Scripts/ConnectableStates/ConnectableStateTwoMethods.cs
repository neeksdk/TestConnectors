using ConnectableComponents;
using UnityEngine;

namespace ConnectableStates
{
    public class ConnectableStateTwoMethods : ConnectableStateBase
    {
        internal ConnectableStateTwoMethods(ConnectableComponentSphere componentSphere) :
            base(componentSphere: componentSphere) { }
        
        private static float _timePassedSinceMouseButtonIsDown = 0f;
        private static bool _isMouseDragging = false;

        public override void StartState() {
            OnSphereSelected += ConnectableMode;
        }

        public override void ExitState() {
            if (ConnectionModeIsOn) { OnSphereSelected(null); }

            OnSphereSelected -= ConnectableMode;
        }

        public override void OnMouseDown() {
            _timePassedSinceMouseButtonIsDown = Time.time;
            _isMouseDragging = true;
            
            if (!ConnectionModeIsOn) {
                ConnectingLine = ConnectableComponentSphereRef.GetConnectingLine();
                ConnectingLine.SetStartPoint(ConnectableComponentSphereRef.transform);
                ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
            }

            OnSphereSelected(ConnectableComponentSphereRef);
        }
        
        public override void OnMouseUp() {
            _isMouseDragging = false;
            if (TimePassedSinceMouseButtonIsDown()) return;
            
            OnSphereSelected(null);
        }

        public override void OnMouseEnter() {
            MouseIsOnConnectableComponent = true;

            if (_isMouseDragging) {
                ConnectableComponentSphereRef.SetMaterialColor(Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR);
            }
            
            if (ConnectionModeIsOn) ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
        }

        public override void OnMouseExit() {
            MouseIsOnConnectableComponent = false;
            
            if (ConnectionModeIsOn) {
                if (_isMouseDragging) {
                    ConnectableComponentSphereRef.SetMaterialColor(!IsSameConnectingLine()
                        ? Constants.SELECTED_CONNECTABLE_INACTIVE_COLOR
                        : Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR);
                }
            } else {
                ConnectableComponentSphereRef.SetMaterialColor(Constants.CONNECTABLE_DEFAULT_COLOR);
            }
        }

        private void ConnectableMode(ConnectableComponentSphere connectableSphereClicked) {
            if (connectableSphereClicked is null) {
                ConnectionModeIsOn = false;
                ConnectableComponentSphereRef.SetMaterialColor(Constants.CONNECTABLE_DEFAULT_COLOR);

                if (IsSameConnectingLine()) {
                    ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform.position, false);
                }

                return;
            }

            if (ConnectionModeIsOn) {
                ConnectionModeIsOn = false;
                ConnectableComponentSphereRef.SetMaterialColor(Constants.CONNECTABLE_DEFAULT_COLOR);
                
                if (IsSameConnectingLine()) {
                    ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform.position, false);
                }
            } else {
                ConnectionModeIsOn = true;
                ConnectableComponentSphereRef.SetMaterialColor(connectableSphereClicked == ConnectableComponentSphereRef
                    ? Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR
                    : Constants.SELECTED_CONNECTABLE_INACTIVE_COLOR);
            }
        }

        public override void Update() {
            if (!ConnectionModeIsOn) return;

            if (Input.GetMouseButtonDown(0) && !_isMouseDragging && !MouseIsOnConnectableComponent) {
                OnSphereSelected(null);
                
                return;
            }
            
            if (IsSameConnectingLine() && _isMouseDragging) { ConnectingLine.SetEndPoint(MousePositionInWorldCoords(), true); }
        }

        private static bool TimePassedSinceMouseButtonIsDown() {
            return Time.time - _timePassedSinceMouseButtonIsDown < Constants.TIME_FOR_SINGLE_MOUSE_CLICK;
        }
        
        private Vector3 MousePositionInWorldCoords() {
            if (Camera.main is null) 
                return ConnectableComponentSphereRef.transform.position;

            Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(Ray, out RaycastHit Hit)) 
                return ConnectableComponentSphereRef.transform.position;

            Vector3 Point = Hit.point;
        
            return Point;
        }
    }
}
