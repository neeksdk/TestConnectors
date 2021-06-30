using ConnectableStates;
using UnityEngine;

namespace ConnectableComponents
{
    public class ConnectableComponentCube : ConnectableComponent
    {
        [SerializeField] private Transform connectableParentTransform;
        private void Start() {
            ChangeState(new MovableState(connectableParentTransform));
        }

        private void OnMouseDown() {
            CurrentState.OnMouseDown();
        }
    
        private void OnMouseDrag() {
            CurrentState.OnMouseDragging();
        }
    
        private void OnMouseUp() {
            CurrentState.OnMouseUp();
        }
    }
}
