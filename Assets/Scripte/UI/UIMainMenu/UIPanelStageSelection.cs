using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelStageSelection : UIPanel
{

    [SerializeField] private UIPanelStageDetail _panelStageDetail;
    [SerializeField] private BpStage _prfStageButton;
    [SerializeField] private Transform _transformButtonHolder;
    [SerializeField] private Button _bpReturn;
    private List<BpStage> _buttons =new List<BpStage>();
    
    private void Awake() {
        _bpReturn.onClick.AddListener(UIPBPressReturn);
        _panelStageDetail.OnPanelClose+= PanelStageDetailOnOnPanelClose;
    }

    private void PanelStageDetailOnOnPanelClose(object sender, EventArgs e) {
        if (_buttons == null || _buttons[0] == null) _bpReturn.Select();
        else _buttons[0].Select();
    }


    private void SetUpButtons() {
        foreach (StageData stageData in GameStateData._stageDatas) {
            BpStage bp =Instantiate(_prfStageButton,  _transformButtonHolder);
            bp.Initiate(stageData);
            bp.OnStageSelected+= BpOnOnStageSelected;
            _buttons.Add(bp);
        }
    }

    private void RemoveButtons() {
        for (int i = _buttons.Count - 1; i >= 0; i--) {
            _buttons[i].OnStageSelected -= BpOnOnStageSelected;
            Destroy(_buttons[i].gameObject,0.01f); 
            _buttons.RemoveAt(i);
        }
    }

    private void BpOnOnStageSelected(object sender, StageData e) {
        _panelStageDetail.SetUpStageData(e);
        _panelStageDetail.OpenPanel();
    }

    private void UIPBPressReturn() {
        RemoveButtons();
        ClosePanel();
    }

    public override void OpenPanel() {
        SetUpButtons();
        base.OpenPanel();
        if (_buttons == null || _buttons[0] == null) _bpReturn.Select();
        else _buttons[0].Select();
    }
}