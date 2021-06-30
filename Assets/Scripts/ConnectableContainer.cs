using UnityEngine;

public class ConnectableContainer : MonoBehaviour
{
    public void SetPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }
}
