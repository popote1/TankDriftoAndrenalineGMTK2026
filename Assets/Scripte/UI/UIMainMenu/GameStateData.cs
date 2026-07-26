using System;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStateData
{
    public const string MAINMENUSCENENAME = "TestMenuBackGround";
    
    public static event EventHandler<int> OnMedalCountChange;
    public static event EventHandler<string> OnCodeSubmition;
    
    public static int MedalScore;
    public static StageData[] _stageDatas;
    public static StageData CurrentLevelSelected;
    // Options
    public static bool PlayMusicInRandom;
    public static bool PlayMusicOnSceneLoad=true;

    public static void GainMadal() {
        MedalScore++;
        OnMedalCountChange.Invoke(null,MedalScore);
    }

    public static void SetupStageData(SoStageData[] soStageDatas) {
        _stageDatas = new StageData[soStageDatas.Length];
        for (int i = 0; i < soStageDatas.Length; i++) {
            _stageDatas[i] = new StageData(soStageDatas[i]);
        }
        Debug.Log("Setup Stage Data");
    }
    public static string GetStingTime(int time) {
        int seconds = time % 60;
        int minute = time / 60;
        return minute + " min  " +seconds + " sec"; 
    }

    public static void LoadLevel(StageData stageData) {
        CurrentLevelSelected = stageData;
        StaticEvent.DoLevelLoading(CurrentLevelSelected.SceneName);       
    }
    
    public static void ReturnToMainMenu() {
        CurrentLevelSelected = null;
        StaticEvent.ResetStaticData();
        StaticEvent.DoLevelLoading(MAINMENUSCENENAME);
    }
}