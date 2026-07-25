using System;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public GameObject UIVisualPanel;
    public event EventHandler OnPanelClose;
    public event EventHandler OnPanelOpen;

    public virtual void OpenPanel() {
        UIVisualPanel.SetActive(true);
        OnPanelOpen?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void ClosePanel() {
        UIVisualPanel.SetActive(false);
        OnPanelClose?.Invoke(this, EventArgs.Empty);
    }
}