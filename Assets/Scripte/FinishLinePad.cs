using UnityEngine;

public class FinishLinePad : MonoBehaviour
{
    [SerializeField] private bool _destroyOnEnter;
    [SerializeField] private int _checkpointNeeded;
    
    [SerializeField] private GameObject _idleVFXplane;
    [SerializeField] private GameObject _idleVFXLock;
    [SerializeField] private GameObject _idleVFXopen;
    [SerializeField]private GameObject _prefabVFXOnDestroy;
    [Space(5)]
    [SerializeField] private AudioElement _sfxOnTake;
    [SerializeField] private AudioElement _sfxOnCanTake;

    private bool _passed = false;
    private void Start() {
        CheckIfOpen();
        StaticEvent.OnCheckPointPass+= StaticEventOnOnCheckPointPass;
    }

    private void OnDestroy()
    {
        StaticEvent.OnCheckPointPass-= StaticEventOnOnCheckPointPass;
    }

    private void StaticEventOnOnCheckPointPass(object sender, int e) {
        CheckIfOpen();
    }

    private void OnTriggerEnter(Collider other) {
        if (_passed)   return;
        if (other.tag == "Player") {
            if (_checkpointNeeded <= StaticEvent.CheckPointPass) {
                StaticEvent.DoOnStageComplete();
                StaticEvent.DoOnBlockPlayerControl(true);
                Vector3 forward =other.transform.forward;
                GameObject VFX =Instantiate(_prefabVFXOnDestroy, other.transform.position, transform.rotation);
                VFX.transform.forward = forward;
                _idleVFXplane.SetActive(false);
                _idleVFXLock.SetActive(false);
                _idleVFXopen.SetActive(false);
                _passed = true;
                if (_destroyOnEnter) Destroy(gameObject);
                if (_sfxOnTake != null && AudioManager.Instance != null) {
                    AudioManager.Instance.PlaySFX(_sfxOnTake);
                }
            }
            else {
                if (_sfxOnTake != null && AudioManager.Instance != null) {
                    AudioManager.Instance.PlaySFX(_sfxOnCanTake);
                }
            }
        }
    }

    private void CheckIfOpen() {
        if (StaticEvent.CheckPointPass < _checkpointNeeded)
        {
            _idleVFXLock.SetActive(true);
            _idleVFXopen.SetActive(false);
        }
        else
        {
            _idleVFXLock.SetActive(false);
            _idleVFXopen.SetActive(true);
        }
    }
}
