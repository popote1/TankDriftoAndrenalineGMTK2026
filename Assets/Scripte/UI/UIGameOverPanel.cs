using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOverPanel: MonoBehaviour
{
    [SerializeField] private Button _bpRestart;
    [SerializeField] private Button _bpFreeRoom;
    [SerializeField] private Transform _panel;

    private void Start() {
        StaticEvent.OnGameOver += StaticEventOnOnGameOver;
        _bpRestart.onClick.AddListener(UIOnRestart);
        _bpFreeRoom.onClick.AddListener(UIOnFreeRoom);
        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        StaticEvent.OnGameOver -= StaticEventOnOnGameOver;
    }

    private void StaticEventOnOnGameOver(object sender, EventArgs e) {
        _panel.gameObject.SetActive(true);
    }

    private void UIOnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UIOnFreeRoom() {
        StaticEvent.DoOnBlockPlayerControl(false);
        _panel.gameObject.SetActive(false);
    }
}