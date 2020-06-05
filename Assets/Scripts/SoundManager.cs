using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    
    [Header("Настройки")]
    public AudioSource efxSource;
    public AudioSource musicSource;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    
    [Header("Музыка для уровней")]
    public AudioClip levelTheme_1;
    public AudioClip levelTheme_2;
    public AudioClip levelTheme_3;

    [Header("Победная музыка")]
    public AudioClip victoryTheme;

    private int _currentLevelId = -1;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    

    public void PlayMusic(int levelBuildId) {
        if (levelBuildId == _currentLevelId) {
            return;
        }
        AudioClip clip;

        _currentLevelId = levelBuildId;
        
        switch (levelBuildId) {
            case 0:
                clip = levelTheme_1;
                break;
            case 1:
                clip = levelTheme_2;
                break;
            case 2:
                clip = levelTheme_3;
                break;
            default:
                clip = levelTheme_1;
                break;
        }

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayWinnMusic() {
        musicSource.Stop();
        musicSource.clip = victoryTheme;
        musicSource.Play();
    }
    
    public void PlaySingle(AudioClip clip) {
        efxSource.clip = clip;
        efxSource.Play();
    }

    
    public void RandomizeSfx(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
