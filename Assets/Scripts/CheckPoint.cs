using UnityEngine;

/// <summary>
/// Активация чекпоинтов
/// </summary>
public class CheckPoint : MonoBehaviour {
    public Material commonMaterial;
    public Material activeMaterial;

    public AudioClip activateSound;

    private bool _isActive;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !_isActive) {
            _isActive = true;
            SoundManager.instance.PlaySingle(activateSound);
            
            if (GameManager.Instance.lastCheckPoint) {
                GameManager.Instance.lastCheckPoint.GetComponent<Renderer>().material = commonMaterial;
            }
            GameManager.Instance.lastCheckPoint = gameObject;
            
            GetComponent<Renderer>().material = activeMaterial;
        }
    }
}
