using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMainMenu : UIPanel {
    [SerializeField] private Button _bpStageSelection;
    [SerializeField] private Button _bpOptions;
    [SerializeField] private Button _bpCredits;
    [SerializeField] private Button _bpQuite;
    [Space(5)]
    [SerializeField] private UIPanel _panelStageSelection;
    [SerializeField] private UIPanel _panelOptions;
    [SerializeField] private UIPanel _panelCredits;
    [SerializeField] private UIPanel _panelQuite;

    private void Awake() {
        _bpStageSelection.onClick.AddListener(UIBPPresStageSelection);
        _bpOptions.onClick.AddListener(UIBPPressOption);
        _bpCredits.onClick.AddListener(UIBPPressCredits);
        _bpQuite.onClick.AddListener(UIBPQuite);
    }

    private void Start() {
        _panelStageSelection.OnPanelClose += StageSelectionClose;
        _panelOptions.OnPanelClose+= OptionPanelClose;
        _panelCredits.OnPanelClose += CreditsPanelClose;
        _panelQuite.OnPanelClose += QuitePanelClose;
        _bpStageSelection.Select();
    }

   

    private void UIBPPresStageSelection() {
        _panelStageSelection.OpenPanel();
        ClosePanel();
    }

    private void UIBPPressOption() {
        _panelOptions.OpenPanel();
        ClosePanel();
    }
    private void UIBPPressCredits() {
        _panelCredits.OpenPanel();
        ClosePanel();
    }
    private void UIBPQuite() {
        _panelQuite.OpenPanel();
        ClosePanel();
    }

    private void StageSelectionClose(object sender, EventArgs e) {
        OpenPanel();
        _bpStageSelection.Select();
    }

    private void OptionPanelClose(object sender, EventArgs e) {
        OpenPanel();
        _bpOptions.Select();
    }

    private void CreditsPanelClose(object sender, EventArgs e)
    {
        OpenPanel();
        _bpCredits.Select();
    }

    private void QuitePanelClose(object sender, EventArgs e)
    {
        OpenPanel();
        _bpQuite.Select();
    }
}