using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectingLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform _startTransform, _endTransform; 

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

    private void Update() {
        if (_startTransform == null || _endTransform == null) return;
        
        _lineRenderer.SetPosition(0, _startTransform.position);
        _lineRenderer.SetPosition(1, _endTransform.position);
    }
}
