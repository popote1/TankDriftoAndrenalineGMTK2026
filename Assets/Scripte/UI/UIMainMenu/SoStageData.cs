using UnityEngine;

[CreateAssetMenu (fileName = "SoStageData", menuName = "So/Stagedata")]
public class SoStageData : ScriptableObject {
    public string StageName;
    public string SceneName;
    [Space (5)]
    public int CreatorTime;
    public int GoldTime;
    public int SilverTime;
    public int BronzeTime;
    [Space(5)]
    public Sprite Sprite;
    public string UnlockCode;
    public int MedalScoreToUnlock;
}