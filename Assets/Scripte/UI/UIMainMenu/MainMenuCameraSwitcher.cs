using System;
using UnityEngine;

public class MainMenuCameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _cameraMainMenu;
    [SerializeField] private GameObject _cameraLevelSelection;
    [SerializeField] private GameObject _cameraOption;
    
    [SerializeField] private UIPanel _mainMenuPanel;
    [SerializeField] private UIPanel _mainStageSelectionPanel;
    [SerializeField] private UIPanel _optionsPanel;

    private void Start()
    {
        _mainMenuPanel.OnPanelOpen+= OnMainMenuOpen;
        _mainStageSelectionPanel.OnPanelOpen += OnStageSelectionOpen;
        _optionsPanel.OnPanelOpen += OnOptionsOpen;
    }

    

    private void TurnOffAllCams() {
        _cameraMainMenu.SetActive(false);
        _cameraLevelSelection.SetActive(false);
        _cameraOption.SetActive(false);
    }

    private void OnMainMenuOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraMainMenu.SetActive(true);
    }
    private void OnStageSelectionOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraLevelSelection.SetActive(true);
    }

    private void OnOptionsOpen(object sender, EventArgs e)
    {
        TurnOffAllCams();
        _cameraOption.SetActive(true);
    }
}