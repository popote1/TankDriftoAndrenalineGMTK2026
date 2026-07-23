using UnityEngine;

public class FinishLinePad : MonoBehaviour
{
    [SerializeField] private bool _destroyOnEnter;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StaticEvent.DoOnStageComplete();
            StaticEvent.DoOnBlockPlayerControl(true);
            if (_destroyOnEnter) Destroy(gameObject);
        }
    }
}