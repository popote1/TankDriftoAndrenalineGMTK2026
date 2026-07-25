using UnityEngine;

public class TankAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _asEngine;
    [SerializeField] private TankController _tankController;
    [SerializeField] private float _drifetTreshold = 0.5f;
    [SerializeField] private AudioSource _asSlide;

    [SerializeField] private float _inPitch =0.5f;
    [SerializeField] private float _maxPitch =2.5f;
    private void Update() {
        _asEngine.pitch = Mathf.Lerp(_inPitch, _maxPitch, _tankController.GetNormalizedSpeed);
        _asSlide.enabled=(_tankController.DriftFactor > _drifetTreshold && _tankController.GetGroundedFactor <0.8f);
    }
}