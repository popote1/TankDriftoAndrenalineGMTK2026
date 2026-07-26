using System;
using UnityEngine;

public class VisualTilte : MonoBehaviour
{
    [SerializeField] private TankController _tankController;
    [SerializeField]private float _maxTiltAngle = 10;
    
    [SerializeField]private Transform turret;

    private void Update() {
        float t = _tankController.TurnDot / 2 + 0.5f;
        float tilte = Mathf.Lerp( _maxTiltAngle, -_maxTiltAngle,t);
        transform.localEulerAngles = new Vector3(0, 0, tilte);

        Vector3 dir = Vector3.Lerp(transform.forward, _tankController.GetLinearVelocity.normalized,
            _tankController.DriftFactor);
        turret.forward = dir;
    }
}