using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField]private float _boostPower;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<TankController>().GiveBoost(_boostPower);
        }
    }
}