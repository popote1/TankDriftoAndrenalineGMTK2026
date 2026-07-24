using UnityEngine;

public class CheckPointPad : MonoBehaviour
{
    public bool UsAsRespownPoint = true;
    [SerializeField] private bool _isDefaultSpawn;
    [SerializeField] private bool _checkPountTaken;
    
    [SerializeField] private GameObject _VfxIdle;
    [SerializeField]private GameObject _prefabVFXOnDestroy;
    [SerializeField] private Vector3 _respawnPositionOffset = new Vector3(0,0.5f,0);
    private Vector3 _savePosition;
    private Quaternion _saveRotation;

    private void Start() 
    {
        if (_isDefaultSpawn) {
            _savePosition = transform.position;
            _saveRotation = transform.rotation;
            _VfxIdle.SetActive(false);
            _checkPountTaken = true;
            StaticEvent.SetDefaultSpawn(this);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (_checkPountTaken) return;
        if (other.tag == "Player") {
            _checkPountTaken = true;
            _savePosition = other.transform.position;
            _saveRotation = other.transform.rotation;
            Vector3 forward =other.transform.forward;
            GameObject VFX =Instantiate(_prefabVFXOnDestroy, other.transform.position, transform.rotation);
            VFX.transform.forward = forward;
            _VfxIdle.SetActive(false);
            StaticEvent.DoOnCheckPointPass(this);
        }
    }

    public void RespawnPlayer(GameObject player) {
        player.transform.position = _savePosition+_respawnPositionOffset;
        player.transform.rotation = _saveRotation;
    }
}