using UnityEngine;

public class Timepad : MonoBehaviour {
    [SerializeField]private int _additionnalTime;
    [SerializeField] private bool _destroyOnEnter;
    [SerializeField] private GameObject _prefabVFXOnDestroy;
    [SerializeField] private AudioElement _sfxOnTake;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            TimeManager.Instance.GetAdditionalTime(_additionnalTime);
            Vector3 forward =other.transform.forward;
            GameObject VFX =Instantiate(_prefabVFXOnDestroy, transform.position, transform.rotation);
            VFX.transform.forward = forward;
            if (_destroyOnEnter) Destroy(gameObject);
            
            if (_sfxOnTake != null && AudioManager.Instance != null) {
                AudioManager.Instance.PlaySFX(_sfxOnTake);
            }
        }
    }
}


