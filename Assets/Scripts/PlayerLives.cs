using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour {
    [Header("UI текст кол-ва жизней")]
    public Text livesLeft;
    
    [Header("UI посмертный текст")]
    public GameObject deathTextContainer;
    
    [Header("UI game over текст")]
    public GameObject gameOverTextContainer;
    
    [Header("Задержка перед смертью")]
    public float beforeDeathTime = 3;

    [Header("Звук смерти")]
    public AudioClip deathSound;
    [Header("Звук геймовера")]
    public AudioClip gameOverSound;

    private Rigidbody _playerRb;
    private bool _isRespawning;
    
    
    private void Start() {
        _playerRb = GetComponent<Rigidbody>();
        SetLivesText();
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.isPlayerDead) {
            OnFall();
        } else {
            if (!_isRespawning) {
                _isRespawning = true;
                RespawnAfterDeath(); 
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Death")) {
            GameManager.Instance.isPlayerDead = true;
        }
    }
    
    private void OnFall() {
        if (transform.position.y < -10) {
            GameManager.Instance.isPlayerDead = true;
        }
    }

    private void RespawnAfterDeath() {
        GameManager.Instance.currentLives--;
        SetLivesText();

        if (GameManager.Instance.currentLives > 0) {
            deathTextContainer.SetActive(true);
            StartCoroutine(Death());
        } else {
            deathTextContainer.SetActive(false);
            gameOverTextContainer.SetActive(true);
            StartCoroutine(GameOver());
        }
    }

    private void SetLivesText() {
        livesLeft.text = $"x {GameManager.Instance.currentLives}";
    }
    
    IEnumerator Death() {
        SoundManager.instance.PlaySingle(deathSound);
        
        yield return new WaitForSeconds(beforeDeathTime);
        
        Vector3 fixPosition = new Vector3(0, 1, 0);
            
        if (GameManager.Instance.lastCheckPoint) {
            transform.position = GameManager.Instance.lastCheckPoint.transform.position + fixPosition;
        } else {
            transform.position = GameManager.Instance.startPoint.transform.position + fixPosition; 
        }
        
        GameManager.Instance.isPlayerDead = false;
        _isRespawning = false;

        _playerRb.velocity = Vector3.zero;
        _playerRb.angularVelocity = Vector3.zero;
        
        deathTextContainer.SetActive(false);
    }

    IEnumerator GameOver() {
        SoundManager.instance.PlaySingle(gameOverSound);
        
        yield return new WaitForSeconds(beforeDeathTime);

        gameOverTextContainer.SetActive(false);
        GameManager.Instance.ResetLevelSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
