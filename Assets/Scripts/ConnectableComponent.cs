using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class ConnectableComponent : MonoBehaviour
{
    [SerializeField] protected ConnectingLine connectingLine;
    protected MeshRenderer ConnectableMeshRender;

    private void Awake() {
        ConnectableMeshRender = GetComponent<MeshRenderer>();
    }
}
