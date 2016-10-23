using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

    [SerializeField]
    private AudioClip dropChipSound;
    [SerializeField]
    private AudioClip pressButtonSound;

    private static AudioSource _audioSource;
    private static Audio _instance = null;
    private static AudioClip _dropChipSound;
    private static AudioClip _pressButtonSound;

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

        _audioSource = GetComponent<AudioSource>();
        Debug.Log("Audio Source: " + _audioSource);

        _dropChipSound = dropChipSound;
        _pressButtonSound = pressButtonSound;
    }

    public static void PlayDropChip() {
        Debug.Log("Drop chip sound: " + _dropChipSound);
        if (_audioSource.clip != _dropChipSound) {
            _audioSource.clip = _dropChipSound;
        }
        _audioSource.Play();
    }

    public static void PlayPressButton() {
        if (_audioSource.clip != _pressButtonSound)
        {
            _audioSource.clip = _pressButtonSound;
        }
        _audioSource.Play();
    }
}
