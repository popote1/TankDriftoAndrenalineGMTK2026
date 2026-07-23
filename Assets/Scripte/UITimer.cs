using DG.Tweening;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animTime = 0.8f;
    [SerializeField] private float _animSize = 1.3f;

    private void Start() {
        StaticEvent.OnTimeChange += InstanceOnOnTimeChange;
    }
    private void OnDestroy() => StaticEvent.OnTimeChange -= InstanceOnOnTimeChange;

    private void InstanceOnOnTimeChange(object sender, int e) {
        _timeText.text = e.ToString();
        _timeText.transform.localScale = new Vector3(_animSize, _animSize, _animSize);
        _timeText.transform.DOScale(1, _animTime).SetEase(_animationCurve);
    }
}