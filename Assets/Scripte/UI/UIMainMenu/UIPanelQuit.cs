using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelQuit: UIPanel
{
    [SerializeField] private Button _bpQuit;
    [SerializeField] private Button _bpReturn;
    
    private void Awake() {
        _bpReturn.onClick.AddListener(UIBPPressReturn);
        _bpQuit.onClick.AddListener(UIBPPressQuite);
    }

    public override void OpenPanel() {
        base.OpenPanel();
        _bpReturn.Select();
    }

    private void UIBPPressQuite() {
        Application.Quit();
    }

    private void UIBPPressReturn() {
        ClosePanel();
    }
}