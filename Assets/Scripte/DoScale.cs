using DG.Tweening;
using UnityEngine;

public class DoScale : MonoBehaviour
{
    [SerializeField] private Vector3 _endScale = new Vector3(1.1f,1.1f,1.1f);
    [SerializeField] private float _animationTime = 1;
    [SerializeField] private AnimationCurve _animationCurve =  AnimationCurve.EaseInOut(0, 0, 1, 1); 

    private void Start() {
        transform.DOScale(_endScale, _animationTime).SetEase(_animationCurve).SetLoops(-1, LoopType.Yoyo);;
    }
}