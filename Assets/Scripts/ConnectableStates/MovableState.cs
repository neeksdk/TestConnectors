using UnityEngine;

namespace ConnectableStates
{
    public class MovableState : MovableStateBase
    {
        private bool _isDragging;
        private Vector3 _mouseOffset;
        private float _cameraZDistance;
        private readonly Transform _connectableParentTransform;
        
        public MovableState(Transform connectableParentTransform) {
            _connectableParentTransform = connectableParentTransform;
        }
        
        public override void OnMouseDown() {
            if ((Camera.main is null)) return;

            Vector3 Position = _connectableParentTransform.position;
            _cameraZDistance = Camera.main.WorldToScreenPoint(Position).z;
            _mouseOffset = Position - GetMouseWorldPoint();

            _isDragging = true;
        }

        public override void OnMouseUp() {
            _isDragging = false;
        }

        public override void OnMouseDragging() {
            if (!_isDragging || Camera.main is null) return;

            Vector3 NewPosition = GetMouseWorldPoint() + _mouseOffset;
            NewPosition.y = 0;

            _connectableParentTransform.position = GetPositionInRadius(positionToCheck: NewPosition);
        }
        
        
        private static Vector3 GetPositionInRadius(Vector3 positionToCheck) {
            float Distance = Vector3.Distance(Vector3.zero, positionToCheck);

            return Distance > Main.Radius
                ? positionToCheck.normalized * Main.Radius
                : positionToCheck;
        }

        private Vector3 GetMouseWorldPoint() {
            Vector3 MousePoint = Input.mousePosition;
            MousePoint.z = _cameraZDistance;

            return Camera.main is null ? Vector3.zero : Camera.main.ScreenToWorldPoint(MousePoint);
        }
    }
}
