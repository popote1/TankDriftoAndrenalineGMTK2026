using System;

public static class StaticEvent
{


    public static int CheckPointPass;
    public static CheckPointPad RespawnPad;
    
    public static event EventHandler OnGameStart;
    public static event EventHandler<int> OnTimeChange;
    public static event EventHandler OnGameOver;
    public static event EventHandler OnStageComplete;
    public static event EventHandler<bool> OnBlockPlayerControl;
    public static event EventHandler<int> OnCheckPointPass;
    public static event EventHandler<string> OnLevelLoading;
    public static event EventHandler<SoMusic> OnSoMusicChange;
    public static event EventHandler OnAskForNextSong;

    public static void DoOnGameStart() {
        OnGameStart?.Invoke(null, EventArgs.Empty);
        CheckPointPass = 0;
    }

    public static void DoOnTimeChange(int value) => OnTimeChange?.Invoke(null, value);
    public static void DoOnGameOver() => OnGameOver?.Invoke(null, EventArgs.Empty);
    public static void DoOnBlockPlayerControl(bool value) => OnBlockPlayerControl?.Invoke(null, value);
    public static void DoOnStageComplete() => OnStageComplete?.Invoke(null, EventArgs.Empty);
    public static void DoOnSoMusicChange(SoMusic value) => OnSoMusicChange?.Invoke(null, value);
    public static void DoOnAskForNextSong() => OnAskForNextSong?.Invoke(null, EventArgs.Empty);

    public static void DoOnCheckPointPass(CheckPointPad pad) {
        CheckPointPass++;
        if( pad.UsAsRespownPoint) RespawnPad = pad;
        OnCheckPointPass?.Invoke(null, CheckPointPass);
    }

    public static void SetDefaultSpawn(CheckPointPad pad) {
        RespawnPad = pad;
    }

    public static void ResetStaticData()
    {
        CheckPointPass = 0;
        RespawnPad = null;
    }

    public static void DoLevelLoading(string sceneName) => OnLevelLoading?.Invoke(null, sceneName);
}
