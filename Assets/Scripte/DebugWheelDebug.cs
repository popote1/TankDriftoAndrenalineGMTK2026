using UnityEngine;

public class DebugWheelDebug : MonoBehaviour
{
    [SerializeField] private float _trailLenght = 0.5f;
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _trailLenght);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * _trailLenght);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _trailLenght);
    }
}