using UnityEngine;

public class ConnectableComponentCube : ConnectableComponent
{
    [SerializeField] private Transform connectableParentTransform;
    private bool _isDragging;
    private Vector3 _mouseOffset;
    private float _cameraZDistance;
    
    private void OnMouseDown() {
        if ((Camera.main is null)) return;

        Vector3 Position = connectableParentTransform.position;
        _cameraZDistance = Camera.main.WorldToScreenPoint(Position).z;
        _mouseOffset = Position - GetMouseWorldPoint();

        _isDragging = true;
    }
    
    private void OnMouseDrag() {
        if (!_isDragging || Camera.main is null) return;

        Vector3 NewPosition = GetMouseWorldPoint() + _mouseOffset;
        NewPosition.y = 0;

        connectableParentTransform.position = GetPositionInRadius(positionToCheck: NewPosition);
    }
    
    private void OnMouseUp() {
        _isDragging = false;
    }
    
    private static Vector3 GetPositionInRadius(Vector3 positionToCheck) {
        float Distance = Vector3.Distance(Vector3.zero, positionToCheck);

        return Distance > Constants.GENERATED_PREFABS_COUNT
            ? positionToCheck.normalized * Constants.GENERATED_PREFABS_COUNT
            : positionToCheck;
    }

    private Vector3 GetMouseWorldPoint() {
        Vector3 MousePoint = Input.mousePosition;
        MousePoint.z = _cameraZDistance;

        return Camera.main is null ? Vector3.zero : Camera.main.ScreenToWorldPoint(MousePoint);
    }
}
