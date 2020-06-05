using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Сбор предметов и отображение очков за сбор
/// </summary>
public class CollectItems : MonoBehaviour {
    public Text countedItemsText;

    public AudioClip pickUpSound1;
    public AudioClip pickUpSound2;

    private void Start() {
        SetCountText();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            SoundManager.instance.RandomizeSfx(pickUpSound1, pickUpSound2);
            other.gameObject.SetActive(false);
            GameManager.Instance.collectedItems++;
            SetCountText();
        }
    }

    private void SetCountText() {
        countedItemsText.text = $"Собрано предметов: {GameManager.Instance.collectedItems.ToString()}";
    }
}
