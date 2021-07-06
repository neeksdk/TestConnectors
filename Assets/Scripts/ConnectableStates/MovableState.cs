using UnityEngine;

namespace ConnectableStates
{
    public class MovableState : MovableStateBase
    {
        private bool _isDragging;
        private Vector3 _mouseOffset;
        private readonly Transform _connectableParentTransform;

        public MovableState(Transform connectableParentTransform) {
            _connectableParentTransform = connectableParentTransform;
        }
        
        public override void OnMouseDown() {
            if ((Camera.main is null)) return;

            _isDragging = true;
        }

        public override void OnMouseUp() {
            _isDragging = false;
        }

        public override void OnMouseDragging() {
            if (!_isDragging || Camera.main is null) return;

            Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] Hits = Physics.RaycastAll(Ray);
            
            int i = 0;
            while (i < Hits.Length) {
                RaycastHit HitInfo = Hits[i];
                if (HitInfo.collider.CompareTag($"PlaneArea"))
                {
                    _connectableParentTransform.position = GetPositionInRadius(HitInfo.point);
                    //_connectableParentTransform.rotation = Quaternion.FromToRotation(Vector3.up, HitInfo.normal);
                }
                
                i++;
            }
        }

        private static Vector3 GetPositionInRadius(Vector3 positionToCheck) {
            float Distance = Vector3.Distance(Vector3.zero, positionToCheck);

            return Distance > Main.Radius
                ? positionToCheck.normalized * Main.Radius
                : positionToCheck;
        }
    }
}
