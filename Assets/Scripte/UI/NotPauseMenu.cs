using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NotPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    
    [SerializeField] private TMP_Text _txtStageName;
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
    [Space(10)]
    [SerializeField] private AudioMixer _audioMixer;
    [Space(5)]
    [SerializeField] private Slider _sliderMasteVolume;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Slider _sliderSFXVolume;
    [SerializeField] private Slider _sliderAmbianceVolume;
    [Space(5)] 
    [SerializeField] private Button _bpReturn;
    [SerializeField] private Button _bpMainMenu;
    [SerializeField] private Button _bpRestart;
    [Space(5)] 
    [SerializeField] private Button _bpInGameMenu;
    
    private InputAction _inputActionEscape;
    
    private void Awake() {
        
        _inputActionEscape = InputSystem.actions.FindAction("Escape");
        _inputActionEscape.started+= InputActionEscapeOnstarted;
        if( GameStateData.CurrentLevelSelected!=null)SetUpStageData(GameStateData.CurrentLevelSelected);
        _sliderMasteVolume.onValueChanged.AddListener(UIChangeMasterVolume);
        _sliderMusicVolume.onValueChanged.AddListener(UIChangeMusicVolume);
        _sliderSFXVolume.onValueChanged.AddListener(UIChangeSFXVolume);
        _sliderAmbianceVolume.onValueChanged.AddListener(UIChangeAmbianceVolume);
        
        _bpReturn.onClick.AddListener(ClosePanel);
        _bpInGameMenu.onClick.AddListener(PressButtonMenu);
        _bpRestart.onClick.AddListener(UIOnRestart);
        _bpMainMenu.onClick.AddListener(UIMainMenu);
    }

    

    private void UIChangeMasterVolume(float value) {
        _audioMixer.SetFloat("VolumeMaster", Mathf.Log10(value) * 20);
    }
    private void UIChangeMusicVolume(float value) {
        _audioMixer.SetFloat("VolumeMusic", Mathf.Log10(value) * 20);
    }
    private void UIChangeSFXVolume(float value) {
        _audioMixer.SetFloat("VolumeSFX", Mathf.Log10(value) * 20);
    }
    private void UIChangeAmbianceVolume(float value) {
        _audioMixer.SetFloat("VolumeAmbiance", Mathf.Log10(value) * 20);
    }
    
    private void SetUpVolumes() {
        _audioMixer.GetFloat("VolumeMaster",out float masterValue);
        _audioMixer.GetFloat("VolumeMusic",out float musicValue);
        _audioMixer.GetFloat("VolumeAmbiance",out float ambianceValue);
        _audioMixer.GetFloat("VolumeSFX",out float sfxValue);
        _sliderMasteVolume.value = Mathf.Exp(masterValue / 20);
        _sliderSFXVolume.value = Mathf.Exp(sfxValue / 20);
        _sliderMusicVolume.value = Mathf.Exp(musicValue / 20);
        _sliderAmbianceVolume.value = Mathf.Exp(ambianceValue / 20);
    }
    
    public void SetUpStageData(StageData stageData) {
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
    
    private void UIOnRestart() {
        GameStateData.LoadLevel(GameStateData.CurrentLevelSelected); 
    }

    private void UIMainMenu() {
        EventSystem.current.enabled = false;
        GameStateData.ReturnToMainMenu();
    }

    public void OpenPanel() {
        _mainPanel.SetActive(true);
    }
    public void ClosePanel() {
        _mainPanel.SetActive(false);
    }
    private void InputActionEscapeOnstarted(InputAction.CallbackContext obj) {
        PressButtonMenu();
    }
    public void PressButtonMenu() {
        if(_mainPanel.activeSelf) ClosePanel();
        else OpenPanel();
    } 
}