using TMPro;
using UnityEngine;

public class UIMedalCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _medalCounter;

    private void Start() {
        _medalCounter.text = "MedaleCount = "+GameStateData.MedalScore.ToString();
    }
}