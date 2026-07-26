using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animTime = 0.8f;
    [SerializeField] private float _animSize = 1.3f;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _imgFill;
    [SerializeField] private AudioElement _aeOnLowTime;
    [Space(5)] 
    [SerializeField] private TMP_Text _txtRunTimer;

    private void Start() {
        StaticEvent.OnTimeChange += InstanceOnOnTimeChange;
        StaticEvent.OnTimeChangeMilSec+= InstanceOnOnTimeChangeMilSec;
    }
    private void OnDestroy() => StaticEvent.OnTimeChange -= InstanceOnOnTimeChange;

    private void InstanceOnOnTimeChange(object sender, int e) {
        _timeText.text = e.ToString();
        _timeText.transform.localScale = new Vector3(_animSize, _animSize, _animSize);
        _timeText.transform.DOScale(1, _animTime).SetEase(_animationCurve);

        float t = e / 60f;
        _imgFill.fillAmount = t;
        _imgFill.color = _gradient.Evaluate(t);

        if (e <= 10) {
            if (AudioManager.Instance != null) {
                AudioManager.Instance.PlaySFX(_aeOnLowTime);
            }
        }

    }
    private void InstanceOnOnTimeChangeMilSec(object sender, int e) {
        int milsec = e%100;
        int sec =  (e/100)%60;
        int min = (e / 6000);

        string timer = min + ": " + sec + " ." + milsec;
        _txtRunTimer.text = timer;
    }
}
