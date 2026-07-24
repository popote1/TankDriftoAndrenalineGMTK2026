using UnityEngine;

public class DriftFeedBack : MonoBehaviour
{
    [SerializeField] private GameObject _PSSparksLeft;
    [SerializeField] private GameObject _PSSparksRight;
    [SerializeField] private TankController _tankController;
    [SerializeField, Range(0,1)] private float _driftThreashold =0.4f;

    private void Update()
    {
        if (_tankController.DriftFactor > _driftThreashold&& _tankController.GetGroundedFactor<0.8f)
        {
            if (_tankController.TurnDot < 0)
            {
                _PSSparksLeft.SetActive(false);
                _PSSparksRight.SetActive(true);
            }
            else
            {
                _PSSparksLeft.SetActive(true);
                _PSSparksRight.SetActive(false);
            }
        }
        else
        {
            _PSSparksLeft.SetActive(false);
            _PSSparksRight.SetActive(false);
        }
    }
}