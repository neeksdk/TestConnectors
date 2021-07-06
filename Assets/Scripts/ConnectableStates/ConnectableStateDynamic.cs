using ConnectableComponents;
using UnityEngine;

namespace ConnectableStates
{
    public class ConnectableStateDynamic : ConnectableStateBase
    {
        internal ConnectableStateDynamic(ConnectableComponentSphere componentSphere) : base(
            componentSphere: componentSphere) { }
    
        private static Transform _storedBeforeEnterConnectionMode;

        public override void StartState() {
            OnSphereSelected += ConnectableMode;
        }

        public override void ExitState() {
            if (ConnectionModeIsOn) { OnSphereSelected(null); }

            OnSphereSelected -= ConnectableMode;
        }

        public override void OnMouseDown() {
            if (ConnectionModeIsOn) return;

            ConnectingLine = ConnectableComponentSphereRef.GetConnectingLine();
            _storedBeforeEnterConnectionMode = ConnectableComponentSphereRef.transform;
            ConnectingLine.SetStartPoint(_storedBeforeEnterConnectionMode);
            ConnectingLine.SetEndPoint(MousePositionInWorldCoords(), true);
            OnSphereSelected(ConnectableComponentSphereRef);
        }

        public override void OnMouseUp() {
            OnSphereSelected(null);
        }

        public override void OnMouseEnter() {
            if (!ConnectionModeIsOn) return;

            ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
            ConnectableComponentSphereRef.SetMaterialColor(Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR);
        }

        public override void OnMouseExit() {
            if (!ConnectionModeIsOn) return;

            ConnectingLine.SetEndPoint(_storedBeforeEnterConnectionMode);
            if (!IsSameConnectingLine()) {
                ConnectableComponentSphereRef.SetMaterialColor(Constants.SELECTED_CONNECTABLE_INACTIVE_COLOR);
            }
        }

        public override void OnMouseDragging() {
            ConnectableComponentSphereRef.SetMaterialColor(Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR);
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
            } else {
                ConnectionModeIsOn = true;
                ConnectableComponentSphereRef.SetMaterialColor(connectableSphereClicked == ConnectableComponentSphereRef
                    ? Constants.SELECTED_CONNECTABLE_ACTIVE_COLOR
                    : Constants.SELECTED_CONNECTABLE_INACTIVE_COLOR);
            }
        }

        public override void Update() {
            if (!ConnectionModeIsOn) return;

            if (IsSameConnectingLine()) { ConnectingLine.SetEndPoint(MousePositionInWorldCoords(), true); }
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
