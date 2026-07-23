using UnityEngine;

public class VisualSuspention : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _maxlength;
    [SerializeField] private Vector3 _offset;

    private Vector3 relativeOffset
    {
        get { return transform.right * _offset.x+transform.up* _offset.y+transform.forward * _offset.z; }
    }
    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, _maxlength, _groundMask)) {
            _visual.position = hit.point+relativeOffset;
        }
        else {
            _visual.position = transform.position+-transform.up*_maxlength+relativeOffset;
        }
    }
}