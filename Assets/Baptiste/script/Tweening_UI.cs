using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.U2D.Physics;

public class Tweening_UI : MonoBehaviour
{
    [SerializeField]private float _duration;
    [SerializeField] private float _angle;
    [SerializeField]private AnimationCurve _curve;
    
    void Start()
    {
        
    transform.DORotate(new Vector3(0f, 0f, _angle), _duration).SetEase(_curve).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
