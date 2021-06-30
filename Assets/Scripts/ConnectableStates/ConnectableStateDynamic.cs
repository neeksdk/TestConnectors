public class ConnectableStateDynamic : ConnectableState
{
    internal ConnectableStateDynamic(ConnectableComponentSphere componentSphere) : base(componentSphere: componentSphere) { }


    public override void StartState() {
        OnSphereSelected += ConnectableMode;
    }

    public override void ExitState() {
        if (ConnectionModeIsOn) {
            OnSphereSelected(null);
        }
        
        OnSphereSelected -= ConnectableMode;
    }

    private void ConnectableMode(ConnectableComponentSphere connectableSphereClicked) {
        if (connectableSphereClicked is null) {
            ConnectionModeIsOn = false;
            ConnectableComponentSphereRef.SetMaterialColor(Constants.CONNECTABLE_DEFAULT_COLOR);

            return;
        }
        
        
    }
}
