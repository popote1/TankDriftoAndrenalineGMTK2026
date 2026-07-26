using System;
using Unity.Cinemachine;
using UnityEngine;

public class MainMenuCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cameraMainMenu;
    [SerializeField] private CinemachineCamera _cameraLevelSelection;
    [SerializeField] private CinemachineCamera _cameraOption;
    [SerializeField] private CinemachineCamera _cameraCredit;
    
    [SerializeField] private UIPanel _mainMenuPanel;
    [SerializeField] private UIPanel _mainStageSelectionPanel;
    [SerializeField] private UIPanel _optionsPanel;
    [SerializeField] private UIPanel _creditPanel;

    private void Start()
    {
        _mainMenuPanel.OnPanelOpen+= OnMainMenuOpen;
        _mainStageSelectionPanel.OnPanelOpen += OnStageSelectionOpen;
        _optionsPanel.OnPanelOpen += OnOptionsOpen;
        _creditPanel.OnPanelOpen += OnCreditOpen;
    }

    

    private void TurnOffAllCams() {
        _cameraMainMenu.Priority.Value=0;
        _cameraLevelSelection.Priority.Value=0;
        _cameraOption.Priority.Value=0;
        _cameraCredit.Priority.Value=0;
    }

    private void OnMainMenuOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraMainMenu.Priority.Value=1;
    }
    private void OnStageSelectionOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraLevelSelection.Priority.Value=1;
    }

    private void OnOptionsOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraOption.Priority.Value=1;
    }
    public void OnCreditOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraCredit.Priority.Value=1;
    }
}