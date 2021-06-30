using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectingLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform _startTransform, _endTransform;
    private Vector3 _mouseFollowingPosition;
    private bool _needToFollowMouse;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
        _startTransform = null;
        _endTransform = null;
    }

    public void SetStartPoint(Transform startTransform) {
        _startTransform = startTransform;
    }

    public void SetEndPoint(Transform endTransform) {
        _endTransform = endTransform;
    }

    public void SetEndPoint(Vector3 endPosition, bool needToFollowMouse) {
        _mouseFollowingPosition = endPosition;
        _needToFollowMouse = needToFollowMouse;
    }

    private void Update() {
        if (_startTransform != null) {
            _lineRenderer.SetPosition(0, _startTransform.position);
        }

        if (_needToFollowMouse) {
            _lineRenderer.SetPosition(1, _mouseFollowingPosition);
        } else {
            if (_endTransform != null) { _lineRenderer.SetPosition(1, _endTransform.position); }
        }
        
    }
}
