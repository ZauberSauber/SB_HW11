using UnityEngine;

public class ConnectorToPlatform : MonoBehaviour {
    public Transform objectsCollector;
    
    private bool _isAppClosing;
    
    private void OnDisable() {
        if (!_isAppClosing) {
            // Убирает объекты с платформы, если её отключают
            objectsCollector.DetachChildren();
        }
    }

    private void OnApplicationQuit() {
        _isAppClosing = true;
    }
    
    private void OnTriggerEnter(Collider other) {
        other.gameObject.transform.SetParent(objectsCollector);
    }
    
    private void OnTriggerExit(Collider other) {
        other.gameObject.transform.SetParent(null);
    }
}
