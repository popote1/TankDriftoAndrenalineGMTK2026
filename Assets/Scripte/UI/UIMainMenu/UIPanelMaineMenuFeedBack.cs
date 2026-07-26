using DG.Tweening;
using UnityEngine;

public class UIPanelMaineMenuFeedBack : MonoBehaviour
{
    [SerializeField] private GameObject _targetPanel;
    [SerializeField] private float _aniamtionTime;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _closeValue;
    [SerializeField] private float _openValue;

    public void ClosePanel() {
        transform.eulerAngles = new Vector3(0, 0, _openValue);
        transform.DOLocalRotate(new Vector3(0, 0, _closeValue), _aniamtionTime).SetEase(_animationCurve).OnComplete(ClosePanelEnd);
    }

    private void ClosePanelEnd() {
        _targetPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        transform.eulerAngles = new Vector3(0, 0, _closeValue);
        transform.DOLocalRotate(new Vector3(0, 0, _openValue), _aniamtionTime).SetEase(_animationCurve);
    }

}