using UnityEngine;

public class Timepad : MonoBehaviour {
    [SerializeField]private int _additionnalTime;
    [SerializeField] private bool _destroyOnEnter;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            TimeManager.Instance.GetAdditionalTime(_additionnalTime);
            if (_destroyOnEnter) Destroy(gameObject);
        }
    }
}


