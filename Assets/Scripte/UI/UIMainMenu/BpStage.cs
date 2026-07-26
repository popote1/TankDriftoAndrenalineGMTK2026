using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BpStage : MonoBehaviour {
    public event EventHandler<StageData> OnStageSelected;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _stageName;
    [SerializeField] private GameObject _panelFoLocked;
    [SerializeField] private TMP_Text _txtMedalNeeded;
    [Header("Debug")]
    [SerializeField] private StageData _stageData;

    public void Initiate(StageData stageData) {
        _stageData = stageData;
        _stageName.text = stageData.StageName;
        
        _button.onClick.AddListener(UIBPPressSelection);
        SetIsLocked(stageData);
    }

    private void SetIsLocked(StageData stageData) {
        _button.interactable = stageData.IsUnlock;
        _panelFoLocked.SetActive(!stageData.IsUnlock);
        _txtMedalNeeded.text = stageData.SoStageData.MedalScoreToUnlock.ToString();
    }

    private void UIBPPressSelection() {
        OnStageSelected?.Invoke(this, _stageData);
    }

    public void Select() => _button.Select();
    
}