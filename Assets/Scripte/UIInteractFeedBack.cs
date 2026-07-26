using DG.Tweening;
using UnityEngine;
public class UIInteractFeedBack : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [Space(10), Header("OpenClosing")]
    [SerializeField] private float _animationTime=0.5f;
    [SerializeField] private AnimationCurve _animationCurveOpeningScaleX = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AnimationCurve _animationCurveClosingScaleX = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private float _yStartPos = 1;
    [SerializeField] private float _yEndPos = 1.7f;
    [SerializeField] private AnimationCurve _animationCurveYPos = AnimationCurve.EaseInOut(0,0,1,1);

    [Space(10), Header("Interaction"), SerializeField]
    private float _animationTimeInteraction =0.2f;
    [SerializeField] private float _endScaleInteract = 2;
    [SerializeField] private AnimationCurve _animationCurveIntractScale = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AnimationCurve _animationCurveIntractAlpha = AnimationCurve.EaseInOut(0,0,1,1);


    public void OpenUpEffect()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _yStartPos, transform.localPosition.z);
        transform.DOPause();
        _canvasGroup.alpha = 1;
        transform.DOScaleX(1, _animationTime).SetEase(_animationCurveOpeningScaleX);
        transform.DOLocalMoveY(_yEndPos, _animationTime).SetEase(_animationCurveYPos);
        
    }

    public void CloseUpEffect()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _yEndPos, transform.localPosition.z);
        transform.DOPause();
        transform.DOScaleX(0, _animationTime).SetEase(_animationCurveClosingScaleX);
        transform.DOLocalMoveY(_yStartPos, _animationTime).SetEase(_animationCurveYPos);
    }

    public void DoInteractEffect()
    {
        transform.DOPause();
        transform.DOScale(_endScaleInteract, _animationTimeInteraction).SetEase(_animationCurveIntractScale);
        _canvasGroup.DOFade(0, _animationTimeInteraction).SetEase(_animationCurveIntractAlpha);
    }
}

