using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStageCompletPanel: MonoBehaviour
{
    [SerializeField] private TMP_Text _txtStageName;
    [SerializeField] private TMP_Text _txtRunTime;
    [SerializeField] private TMP_Text _txtTimeLeft;
    [SerializeField] private GameObject _newBestTime;
    
    [SerializeField] private Button _bpReturnToMainMenu;
    [SerializeField] private Button _bpRestart;
    [SerializeField] private Button _bpFreeRoom;
    [SerializeField] private Transform _panel;
    [Space(5)]
    [SerializeField] private GameObject _creatorMedal;
    [SerializeField] private GameObject _goldMedal;
    [SerializeField] private GameObject _silverMedal;
    [SerializeField] private GameObject _bronzeMedal;
    [Space(5)]
    [SerializeField] private GameObject _gainCreatorMedal;
    [SerializeField] private GameObject _gaingoldMedal;
    [SerializeField] private GameObject _gainSilverMedal;
    [SerializeField] private GameObject _gainBronzeMedal;
    
    

    private void Start() {
        StaticEvent.OnStageComplete += StaticEventOnOnGameOver;
        
        _bpReturnToMainMenu.onClick.AddListener(UIMainMenu);
        _bpRestart.onClick.AddListener(UIOnRestart);
        _bpFreeRoom.onClick.AddListener(UIOnFreeRoom);
        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        StaticEvent.OnStageComplete -= StaticEventOnOnGameOver;
    }

    private void StaticEventOnOnGameOver(object sender, EventArgs e) {
        UpdateStageData();
        _panel.gameObject.SetActive(true);
    }

    private void UIOnRestart() {
        GameStateData.LoadLevel(GameStateData.CurrentLevelSelected); 
    }

    private void UIMainMenu() {
        EventSystem.current.enabled = false;
        GameStateData.ReturnToMainMenu();
    }

    private void UIOnFreeRoom() {
        StaticEvent.DoOnBlockPlayerControl(false);
        _panel.gameObject.SetActive(false);
    }

    private void UpdateStageData() {
        if (GameStateData.CurrentLevelSelected == null) return;
        StageData data = GameStateData.CurrentLevelSelected;
        int runTime = Mathf.FloorToInt(TimeManager.Instance.RunTime);
        
        _txtStageName.text = data.StageName;
        _txtRunTime.text = GameStateData.GetStingTime(runTime);
        _txtTimeLeft.text = GameStateData.GetStingTime(Mathf.FloorToInt(TimeManager.Instance.LeftTime));
        
        _creatorMedal.SetActive(data.CreatorMedal);
        _goldMedal.SetActive(data.GoldMedal);
        _silverMedal.SetActive(data.SilverMedal);
        _bronzeMedal.SetActive(data.BronzeMedal);

        if (data.UnlockCreatorMedal(runTime)) {
            _creatorMedal.SetActive(true);
            _gainCreatorMedal.SetActive(true);
        }
        if (data.UnlockGoldMedal(runTime)) {
            _goldMedal.SetActive(true);
            _gaingoldMedal.SetActive(true);
        }
        if (data.UnlockSilverMedal(runTime)) {
            _silverMedal.SetActive(true);
            _gainSilverMedal.SetActive(true);
        }
        if (data.UnlockBronzeMedal(runTime)) {
            _bronzeMedal.SetActive(true);
            _gainBronzeMedal.SetActive(true);
        }
        _newBestTime.SetActive(data.IsNewBestTime(runTime)); 
        data.SetNewTime(runTime);
        }
    
}
