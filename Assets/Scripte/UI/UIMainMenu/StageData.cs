using System;
using UnityEngine;

[Serializable]
public class StageData {
    public SoStageData SoStageData;
    public bool IsUnlock;
    public int BestTime = int.MaxValue;

    public bool CreatorMedal;
    public bool GoldMedal;
    public bool SilverMedal;
    public bool BronzeMedal;


    public string StageName { get => SoStageData.StageName; }
    public string SceneName { get => SoStageData.SceneName; }
    public int CreatorTime { get => SoStageData.CreatorTime; }
    public int GoldTime { get => SoStageData.GoldTime; }
    public int SilverTime { get => SoStageData.SilverTime; }
    public int BronzeTime { get => SoStageData.BronzeTime; }
    
    public StageData(SoStageData soStageData) {
        SoStageData = soStageData;
        IsUnlock = GameStateData.MedalScore >= SoStageData.MedalScoreToUnlock;
        GameStateData.OnMedalCountChange += CheckIfUnlock;
        GameStateData.OnCodeSubmition+= GameStateDataOnOnCodeSubmition;
    }

    private void GameStateDataOnOnCodeSubmition(object sender, string pass) {
        if (pass == SoStageData.UnlockCode) {
            IsUnlock = true;  
        } 
    }

    private void CheckIfUnlock(object sender, int e) {
        IsUnlock = GameStateData.MedalScore >= SoStageData.MedalScoreToUnlock;
    }

    public void SetNewTime(int newtime) {
        if (!BronzeMedal && newtime < SoStageData.BronzeTime) {
            BronzeMedal = true;
            GameStateData.GainMadal();
        }
        if (!SilverMedal && newtime < SoStageData.SilverTime) {
            SilverMedal = true;
            GameStateData.GainMadal();
        }
        if (!GoldMedal && newtime < SoStageData.GoldTime) {
            GoldMedal = true;
            GameStateData.GainMadal();
        }
        if (!CreatorMedal && newtime < SoStageData.CreatorTime) {
            CreatorMedal = true;
            GameStateData.GainMadal();
        }
        if( newtime< BestTime) BestTime = newtime;
    }
}