using ConnectableStates;
using UnityEngine;

namespace ConnectableComponents
{
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class ConnectableComponent : MonoBehaviour
    {
        [SerializeField] protected ConnectingLine connectingLine;
        protected MeshRenderer ConnectableMeshRender;
        protected IConnectableState CurrentState;

        private void Awake() {
            ConnectableMeshRender = GetComponent<MeshRenderer>();
        }

        protected void ChangeState(IConnectableState newState) {
            if (CurrentState == newState) return;

            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.StartState();
        }
    }
}
