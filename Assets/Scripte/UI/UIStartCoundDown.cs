using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIStartCoundDown : MonoBehaviour {
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private int _countDownLength=4;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animTime = 0.8f;
    [SerializeField] private float _animSize = 1.3f;
    [SerializeField] private AnimationCurve _finalTweenCurve;
    [SerializeField] private float _finalTweenTime = 2f;

    private float _currentTime = 0;
    private int _currentSec = 0;
    [SerializeField]private bool _doCoundDown = true;

    private void Start() {
        _currentTime = _countDownLength;
        _currentSec = Mathf.FloorToInt(_currentTime);
        _canvasGroup.alpha = 1;
    }

    private void Update() {
        if (!_doCoundDown) return;
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0) {
            _doCoundDown= false;
            TimeManager.Instance.StartGame();
            DisplayTime("GO!");
            _canvasGroup.DOFade(0, _finalTweenTime).SetEase(_finalTweenCurve);
            return;
        }
        if (Mathf.FloorToInt(_currentTime) != _currentSec) {
            _currentSec = Mathf.FloorToInt(_currentTime);
            DisplayTime((_currentSec+1).ToString());
        }
    }

    private void DisplayTime(string text) {
        _timeText.text = text;
        _timeText.transform.localScale = new Vector3(_animSize, _animSize, _animSize);
        _timeText.transform.DOScale(1, _animTime).SetEase(_animationCurve);
    }
}