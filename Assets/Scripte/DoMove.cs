using DG.Tweening;
using UnityEngine;

public class DoMove : MonoBehaviour
{
    [SerializeField] private Vector3 _endScale = new Vector3(0,1f,0);
    [SerializeField] private float _animationTime = 1;
    [SerializeField] private AnimationCurve _animationCurve =  AnimationCurve.EaseInOut(0, 0, 1, 1); 

    private void Start() {
        transform.DOLocalMove(_endScale, _animationTime).SetEase(_animationCurve).SetLoops(-1, LoopType.Restart);;
    }
}