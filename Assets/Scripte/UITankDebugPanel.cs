using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITankDebugPanel : MonoBehaviour {
    [SerializeField] private TankController _tankController;
    [SerializeField] private Image _imgnormalizeSpeed;
    [SerializeField] private TMP_Text _txtSpeed;
    [SerializeField] private TMP_Text _txtFrontGrip;
    [SerializeField] private TMP_Text _txtBackGrip;
    [SerializeField] private TMP_Text _txtDriftFactor;
    [SerializeField] private Image _imgLeftDrift;
    [SerializeField] private Image _imgRightDrift;

    private void Update() {
        if (_tankController == null) return;
        _imgnormalizeSpeed.fillAmount = _tankController.GetNormalizedSpeed;
        _txtSpeed.text = Mathf.RoundToInt(_tankController.GetCurrentSpeed).ToString();
        _txtFrontGrip.text = Mathf.RoundToInt(_tankController.GetGurentFrontGrip).ToString();
        _txtBackGrip.text = Mathf.RoundToInt(_tankController.GetGurentBackGrip).ToString();
        _txtDriftFactor.text = Mathf.RoundToInt(_tankController.DriftFactor*100).ToString();

        if (_tankController.TurnDot < 0)
        {
            _imgLeftDrift.fillAmount = Mathf.Abs(_tankController.TurnDot);
            _imgRightDrift.fillAmount = 0;
        }
        else
        {
            _imgLeftDrift.fillAmount =0;
            _imgRightDrift.fillAmount = _tankController.TurnDot;
        }
    }
}