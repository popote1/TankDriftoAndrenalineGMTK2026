using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISpeedMeter : MonoBehaviour
{
    [SerializeField] private Image _imgFill;
    [Space(5)]
    [SerializeField] private float _minRoration;
    [SerializeField] private float _maxRoration;
    [SerializeField] private Image _imgAiguille;
    [Space(5)]
    [SerializeField] private TMP_Text _txtSpeed;
    [SerializeField] private float _speedValueMultiplyer = 230;

    private void Awake() {
        StaticEvent.OnChangeTankNormalizeSpeed+= StaticEventOnOnChangeTankNormalizeSpeed;
    }

    private void OnDestroy()
    {
        StaticEvent.OnChangeTankNormalizeSpeed-= StaticEventOnOnChangeTankNormalizeSpeed;
    }

    private void StaticEventOnOnChangeTankNormalizeSpeed(object sender, float e)
    {
        _imgFill.fillAmount = e;
        _imgAiguille.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(_minRoration, _maxRoration, e));
        _txtSpeed.text = Mathf.FloorToInt(e * _speedValueMultiplyer).ToString();
    }

        
}