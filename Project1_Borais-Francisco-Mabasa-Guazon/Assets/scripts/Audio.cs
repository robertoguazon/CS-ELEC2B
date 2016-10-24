using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

    [SerializeField]
    private AudioClip backgroundMusic;
    [SerializeField]
    private AudioClip dropChipSound;
    [SerializeField]
    private AudioClip pressButtonSound;
    [SerializeField]

    private static AudioSource _soundEffectsSource;
    private static AudioSource _backgroundMusicSource;

    private static AudioClip _backgroundMusic;
    private static AudioClip _dropChipSound;
    private static AudioClip _pressButtonSound;

    private static Audio _instance = null;

    void Awake() {
        if (_instance == null) {

            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("No instance");
        } else if (_instance != this) {
            Destroy(this.gameObject);
            Debug.Log("Has instance");
            return;
        }

        Debug.Log("Instance: " + _instance);
    }

	// Use this for initialization
	void Start () {
        _soundEffectsSource = gameObject.AddComponent<AudioSource>();
        _backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        Debug.Log("Audio Source: " + _soundEffectsSource);

        _dropChipSound = dropChipSound;
        _pressButtonSound = pressButtonSound;
        _backgroundMusic = backgroundMusic;

        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.volume = 0.5f;
        PlayBackgroundMusic(); // start playing
    }

    public static void PlayDropChip() {
        Debug.Log("Drop chip sound: " + _dropChipSound);
        if (_soundEffectsSource.clip != _dropChipSound) {
            _soundEffectsSource.clip = _dropChipSound;
        }
        _soundEffectsSource.Play();
    }

    public static void PlayPressButton() {
        if (_soundEffectsSource.clip != _pressButtonSound)
        {
            _soundEffectsSource.clip = _pressButtonSound;
        }
        _soundEffectsSource.Play();
    }

    public static void PlayBackgroundMusic() {
        if (_backgroundMusicSource.clip != _backgroundMusic) {
            _backgroundMusicSource.clip = _backgroundMusic;
        }

        _backgroundMusicSource.Play();
    }
}
