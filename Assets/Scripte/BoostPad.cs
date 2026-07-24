using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField]private float _boostPower;
    [SerializeField] private GameObject _prefabVFXOnDestroy;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<TankController>().GiveBoost(_boostPower);
            Vector3 forward =other.transform.forward;
            GameObject VFX =Instantiate(_prefabVFXOnDestroy, transform.position, transform.rotation);
            VFX.transform.forward = forward;
        }
    }
}