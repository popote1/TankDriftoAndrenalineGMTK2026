using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelStageDetail : UIPanel
{

    [SerializeField] [CanBeNull] private TMP_Text _txtStageName;
    [SerializeField] private TMP_Text _txtCreatorTime;
    [SerializeField] private TMP_Text _txtGoldTime;
    [SerializeField] private TMP_Text _txtSilverTime;
    [SerializeField] private TMP_Text _txtBronzeTime;
    [Space(5)] 
    [SerializeField] private GameObject _CheckcreatorMedal;
    [SerializeField] private GameObject _CheckGoldMedal;
    [SerializeField] private GameObject _CheckSilverMedal;
    [SerializeField] private GameObject _CheckBronzeMedal;
    [Space(5)] 
    [SerializeField] private TMP_Text _txtBestTime;
    [SerializeField] private Button _bpStart;
    [SerializeField] private Button _bpReturn;
    [Header("Debug")]
    [SerializeField] private StageData _currentStageData; 
    
    private void Awake() {
        _bpStart.onClick.AddListener(UIPressStart);
        _bpReturn.onClick.AddListener(UIPBPressReturn);
    }

    private void UIPBPressReturn() {
        ClosePanel();
    }

    private void UIPressStart() {
        _bpStart.interactable = false;
        _bpReturn.interactable = false;
        GameStateData.LoadLevel(_currentStageData);
    }

    public override void OpenPanel() {
        base.OpenPanel();
        _bpReturn.Select();
    }

    public void SetUpStageData(StageData stageData) {
        _currentStageData = stageData;
        _txtStageName.text = stageData.StageName;
        _txtCreatorTime.text = GameStateData.GetStingTime(stageData.CreatorTime);
        _txtGoldTime.text = GameStateData.GetStingTime(stageData.GoldTime);
        _txtSilverTime.text = GameStateData.GetStingTime(stageData.SilverTime);
        _txtBronzeTime.text = GameStateData.GetStingTime(stageData.BronzeTime);
        
        _CheckcreatorMedal.SetActive(stageData.CreatorMedal);
        _CheckGoldMedal.SetActive(stageData.GoldMedal);
        _CheckSilverMedal.SetActive(stageData.SilverMedal);
        _CheckBronzeMedal.SetActive(stageData.BronzeMedal);

        if (stageData.BestTime < int.MaxValue)
            _txtBestTime.text = GameStateData.GetStingTime(stageData.BestTime);
        else
            _txtBestTime.text = "...";
    }
}