using UnityEngine;

public class AudioAmbianceOnSceneStart : MonoBehaviour {
    [SerializeField] private AudioElement _audioElement;

    public void Start() {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.PlayAmbiance(_audioElement.GetSound());
        }
    }
}