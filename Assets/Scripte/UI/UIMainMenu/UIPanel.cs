using System;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public GameObject UIVisualPanel;
    public UIPanelMaineMenuFeedBack UIPanelFeedBack;
    public event EventHandler OnPanelClose;
    public event EventHandler OnPanelOpen;

    public virtual void OpenPanel() {
        UIVisualPanel.SetActive(true);
        if(UIPanelFeedBack != null)UIPanelFeedBack.OpenPanel();
        OnPanelOpen?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void ClosePanel() {
        if(UIPanelFeedBack != null)UIPanelFeedBack.ClosePanel();
        else UIVisualPanel.SetActive(false);
        OnPanelClose?.Invoke(this, EventArgs.Empty);
    }
}