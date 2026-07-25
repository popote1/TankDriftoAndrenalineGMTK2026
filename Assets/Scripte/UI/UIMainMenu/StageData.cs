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
        if (UnlockBronzeMedal(newtime))  {
            BronzeMedal = true;
            GameStateData.GainMadal();
        }
        if (UnlockSilverMedal(newtime))  {
            SilverMedal = true;
            GameStateData.GainMadal();
        }
        if (UnlockGoldMedal(newtime))  {
            GoldMedal = true;
            GameStateData.GainMadal();
        }
        if (UnlockCreatorMedal(newtime)) {
            CreatorMedal = true;
            GameStateData.GainMadal();
        }
        if( newtime< BestTime) BestTime = newtime;
    }

    public bool UnlockCreatorMedal(int time) => !CreatorMedal && time < SoStageData.CreatorTime;
    public bool UnlockGoldMedal(int time) => !GoldMedal && time < SoStageData.GoldTime;
    public bool UnlockSilverMedal(int time) => !SilverMedal && time < SoStageData.SilverTime;
    public bool UnlockBronzeMedal(int time) => !BronzeMedal && time < SoStageData.BronzeTime;
    public bool IsNewBestTime(int time) => BestTime > time; 

}