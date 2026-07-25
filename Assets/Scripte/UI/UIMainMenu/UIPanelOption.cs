using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIPanelOption : UIPanel
{
    [SerializeField] private AudioMixer _audioMixer;
    [Space(5)]
    [SerializeField] private Slider _sliderMasteVolume;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Slider _sliderSFXVolume;
    [SerializeField] private Slider _sliderAmbianceVolume;
    [Space(5)]
    [SerializeField] private Button _bpReturn;
    
    
    private void Awake() {
        _sliderMasteVolume.onValueChanged.AddListener(UIChangeMasterVolume);
        _sliderMusicVolume.onValueChanged.AddListener(UIChangeMusicVolume);
        _sliderSFXVolume.onValueChanged.AddListener(UIChangeSFXVolume);
        _sliderAmbianceVolume.onValueChanged.AddListener(UIChangeAmbianceVolume);
        _bpReturn.onClick.AddListener(UIPBPressReturn);
    }

    private void UIPBPressReturn() {
        ClosePanel();
    }

    public override void OpenPanel() {
        base.OpenPanel();
        SetUpVolumes();
        _sliderMasteVolume.Select();
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
}