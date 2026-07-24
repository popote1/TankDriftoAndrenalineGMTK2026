using System;

public static class StaticEvent
{


    public static int CheckPointPass;
    public static CheckPointPad _respawnPad;
    
    public static event EventHandler OnGameStart;
    public static event EventHandler<int> OnTimeChange;
    public static event EventHandler OnGameOver;
    public static event EventHandler OnStageComplete;
    public static event EventHandler<bool> OnBlockPlayerControl;
    public static event EventHandler<int> OnCheckPointPass;

    public static void DoOnGameStart() {
        OnGameStart?.Invoke(null, EventArgs.Empty);
        CheckPointPass = 0;
    }

    public static void DoOnTimeChange(int value) => OnTimeChange?.Invoke(null, value);
    public static void DoOnGameOver() => OnGameOver?.Invoke(null, EventArgs.Empty);
    public static void DoOnBlockPlayerControl(bool value) => OnBlockPlayerControl?.Invoke(null, value);
    public static void DoOnStageComplete() => OnStageComplete?.Invoke(null, EventArgs.Empty);

    public static void DoOnCheckPointPass(CheckPointPad pad) {
        CheckPointPass++;
        _respawnPad = pad;
        OnCheckPointPass?.Invoke(null, CheckPointPass);
    }

    public static void SetDefaultSpawn(CheckPointPad pad) {
        _respawnPad = pad;
    }
}

