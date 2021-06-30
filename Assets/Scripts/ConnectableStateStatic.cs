using UnityEngine;

public class ConnectableStateStatic : ConnectableState
{
    internal ConnectableStateStatic(ConnectableComponentSphere componentSphere) : base(componentSphere: componentSphere) { }

    public override void StartState() {
        OnSphereSelected += ConnectableMode;
    }

    public override void ExitState() {
        OnSphereSelected -= ConnectableMode;
    }

    public override void OnMouseDown() {
        if (!ConnectionModeIsOn) {
            ConnectingLine = ConnectableComponentSphereRef.GetConnectingLine();
            ConnectingLine.SetStartPoint(ConnectableComponentSphereRef.transform);
            ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
        }
        OnSphereSelected(ConnectableComponentSphereRef);
    }

    public override void OnMouseOver() {
        if (ConnectionModeIsOn) {
            ConnectableComponentSphereRef.SetMaterialColor(Color.yellow);
        }
    }

    public override void OnMouseExit() {
        if (ConnectionModeIsOn) {
            ConnectableComponentSphereRef.SetMaterialColor(Color.blue);
        }
    }
    
    private void ConnectableMode(ConnectableComponentSphere connectableSphereClicked) {
        if (ConnectionModeIsOn) {
            ConnectionModeIsOn = false;
            ConnectableComponentSphereRef.SetMaterialColor(Color.white);
            if (connectableSphereClicked == ConnectableComponentSphereRef) {
                ConnectingLine.SetEndPoint(ConnectableComponentSphereRef.transform);
            }
        } else {
            ConnectionModeIsOn = true;
            ConnectableComponentSphereRef.SetMaterialColor(connectableSphereClicked == ConnectableComponentSphereRef ? Color.yellow : Color.blue);
        }
    }
}
