using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

    [SerializeField]
    private AudioClip backgroundMusic;
    [SerializeField]
    private AudioClip dropChipSound;
    [SerializeField]
    private AudioClip pressMenuButtonSound;
    [SerializeField]
    private AudioClip pressGameButtonSound;
    [SerializeField]
    private float musicVolume = 0.1f;
    [SerializeField]
    private float soundVolume = 1.0f;

    private static AudioSource _soundEffectsSource;
    private static AudioSource _backgroundMusicSource;

    private static AudioClip _backgroundMusic;
    private static AudioClip _dropChipSound;
    private static AudioClip _pressMenuButtonSound;
    private static AudioClip _pressGameButtonSound;

    private static Audio _instance = null;

    private static float _musicVolume;
    private static float _soundVolume;

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
        _pressMenuButtonSound = pressMenuButtonSound;
        _backgroundMusic = backgroundMusic;
        _pressGameButtonSound = pressGameButtonSound;

        //fields
        _musicVolume = musicVolume;
        _soundVolume = soundVolume;

        _soundEffectsSource.volume = _soundVolume;

        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.volume = _musicVolume;
        PlayBackgroundMusic(); // start playing
    }

    public static void PlayDropChip() {
        Debug.Log("Drop chip sound: " + _dropChipSound);
        if (_soundEffectsSource.clip != _dropChipSound) {
            _soundEffectsSource.clip = _dropChipSound;
        }
        _soundEffectsSource.Play();
    }

    public static void PlayPressMenuButton() {
        if (_soundEffectsSource.clip != _pressMenuButtonSound)
        {
            _soundEffectsSource.clip = _pressMenuButtonSound;
        }
        _soundEffectsSource.Play();
    }

    public static void PlayBackgroundMusic() {
        if (_backgroundMusicSource.clip != _backgroundMusic) {
            _backgroundMusicSource.clip = _backgroundMusic;
        }
        _backgroundMusicSource.Play();
    }

    public static void PlayPressGameButton() {
        if (_soundEffectsSource.clip != _pressGameButtonSound)
        {
            _soundEffectsSource.clip = _pressGameButtonSound;
        }
        _soundEffectsSource.Play();
    }
}
