using UnityEngine;
using UnityEngine.UI;

public class UIPanelCredit : UIPanel {
    [SerializeField] private Button _bpReturn;
    
    private void Awake() {
        _bpReturn.onClick.AddListener(UIPBPressReturn);
    }

    private void UIPBPressReturn()
    {
        ClosePanel();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        _bpReturn.Select();
    }
}