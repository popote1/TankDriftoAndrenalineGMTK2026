using UnityEngine;

public class VisualTilte : MonoBehaviour
{
    [SerializeField] private TankController _tankController;
    [SerializeField]private float _maxTiltAngle = 10;

    private void Update()
    {
        float t = _tankController.TurnDot / 2 + 0.5f;
        Debug.unityLogger.Log("time", t);
        float tilte = Mathf.Lerp( _maxTiltAngle, -_maxTiltAngle,t);
        transform.localEulerAngles = new Vector3(0, 0, tilte);
    }
}