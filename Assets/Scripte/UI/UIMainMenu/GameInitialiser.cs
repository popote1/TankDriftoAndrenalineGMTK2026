using System.Collections.Generic;
using UnityEngine;

public class GameInitialiser : MonoBehaviour {
    public List<SoStageData> SoStageDatas;

    public void Start() {
        if (GameStateData._stageDatas == null) {
            GameStateData.SetupStageData(SoStageDatas.ToArray());
        }
    }
}