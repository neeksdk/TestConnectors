using ConnectableComponents;

namespace ConnectableStates
{
    public class ConnectableStateStatic : ConnectableStateBase
    {
        internal ConnectableStateStatic(ConnectableComponentSphere componentSphere) :
            base(componentSphere: componentSphere) { }

        public override void StartState() {
            OnSphereSelected += ConnectableMode;
        }

        public override void ExitState() {
            if (ConnectionModeIsOn) { OnSphereSelected(null); }

            OnSphereSelected -= ConnectableMode;
        }

        public override void OnMouseDown() {
            if (!ConnectionModeIsOn) {
                ConnectingLine = ConnectableComponentSphereRef.GetConnectingLine();
                ConnectingLine.SetStartPoint(ConnectableComponentSphereRef.transform);
                ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
            } else {
                if (ConnectionModeIsOn) ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
            }

            OnSphereSelected(ConnectableComponentSphereRef);
        }

        public override void OnMouseEnter() {
            MouseIsOnConnectableComponent = true;
        }

        public override void OnMouseExit() {
            MouseIsOnConnectableComponent = false;
        }

        private void ConnectableMode(ConnectableComponentSphere connectableSphereClicked) {
            if (connectableSphereClicked is null) {
                ConnectionModeIsOn = false;
                ConnectableComponentSphereRef.SetMaterialColor(Constants.CONNECTABLE_DEFAULT_COLOR);

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
            if (UnityEngine.Input.GetMouseButtonDown(0) && !MouseIsOnConnectableComponent)
                OnSphereSelected(null);
        }
    }
}
