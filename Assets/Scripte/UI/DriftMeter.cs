using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DriftMeter : MonoBehaviour
{
    [SerializeField] private Image _imgFill;
    [SerializeField] private float _timeGain = 5;
    [SerializeField] private float _scoreToGaine = 4;
    [SerializeField] private float _driftFactorThreshold = 0.5f;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private AudioElement _aeOnGain;
    [Header("Animation")] [SerializeField] private CanvasGroup _aniamtedElement;
    [SerializeField] private float _animationTime = 2;
    [SerializeField] private AnimationCurve _animationSizeCurve;
    [SerializeField] private AnimationCurve _animationAlphaCurve;
    [SerializeField] private float animationsize = 2;


    private float _driftFactor;
    private float _groundedFactor;
    private float _currentScore;

    private void Awake()
    {
        StaticEvent.OnChangeTankDriftFactor += StaticEventOnOnChangeTankDriftFactor;
        StaticEvent.OnChangeTankGroundFactor += StaticEventOnOnChangeTankGroundFactor;
    }

    private void StaticEventOnOnChangeTankGroundFactor(object sender, float e)
    {
        _groundedFactor = e;
    }

    private void StaticEventOnOnChangeTankDriftFactor(object sender, float e)
    {
        _driftFactor = e;
    }

    private void Update()
    {
        if (_driftFactor > _driftFactorThreshold && _groundedFactor < 0.8f)
        {
            _currentScore += Time.deltaTime;

            _imgFill.fillAmount = _currentScore / _scoreToGaine;
            _imgFill.color = _gradient.Evaluate(_currentScore / _scoreToGaine);
            if (_currentScore >= _scoreToGaine)
            {
                _currentScore = 0;
                TimeManager.Instance.GetAdditionalTime(_timeGain);
                _aniamtedElement.transform.localScale = Vector3.one;
                _aniamtedElement.alpha = 0;
                _aniamtedElement.transform
                    .DOScale(new Vector3(animationsize, animationsize, animationsize), _animationTime)
                    .SetEase(_animationSizeCurve);
                _aniamtedElement.DOFade(1, _animationTime).SetEase(_animationAlphaCurve);
            }

        }
    }
}